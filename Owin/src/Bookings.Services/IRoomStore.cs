using System.Collections.Generic;

namespace Bookings.Services
{
    public interface IRoomStore
    {
        IEnumerable<Room> GetRooms();
    }
}