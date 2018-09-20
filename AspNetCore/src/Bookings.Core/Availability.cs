using System;

namespace Bookings.Core
{
    public class Availability
    {
        public int Price { get; }
        public Room Room { get; }
        public DateTimeOffset From { get; }
        public DateTimeOffset To { get; }

        public Availability(Room room, DateTimeOffset @from, DateTimeOffset to, int price)
        {
            Room = room;
            From = @from;
            To = to;
            Price = price;
        }
    }
}
