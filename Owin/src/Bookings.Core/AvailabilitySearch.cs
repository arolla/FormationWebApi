using System;

namespace Bookings.Core
{
    public class AvailabilitySearch
    {
        public AvailabilitySearch(DateTimeOffset from, DateTimeOffset to, int? roomCapacity)
        {
            To = to;
            From = from;
            RoomCapacity = roomCapacity;
        }

        public DateTimeOffset To { get; }
        public DateTimeOffset From { get; }
        public int? RoomCapacity { get; }
    }
}