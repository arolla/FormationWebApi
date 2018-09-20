using System;
using System.Collections.Generic;
using System.Text;

namespace Bookings.Services
{
    public class InMemoryRoomStore : IRoomStore
    {
        private static readonly IDictionary<int, StoredRoom> rooms;

        static InMemoryRoomStore()
        {
            rooms = new Dictionary<int, StoredRoom>();

            for (int i = 1; i <= 10; i++)
            {
                rooms.Add(
                    i, new StoredRoom(i, i, i * 13));
            }
        }

        public IEnumerable<StoredRoom> GetRooms()
        {
            return rooms.Values;
        }

        public StoredRoom GetRoom(int id)
        {
            return rooms[id];
        }
    }
}
