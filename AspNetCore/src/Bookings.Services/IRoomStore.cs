using System.Collections.Generic;
using Bookings.Core;

namespace Bookings.Services
{
    public interface IRoomStore
    {
        IEnumerable<StoredRoom> GetRooms();
        StoredRoom GetRoom(int id);
    }
}