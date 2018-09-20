using System;

namespace Bookings.Core
{
    public class AvailabilitySearch
    {
        public DateTimeOffset To { get; private set; }
        public DateTimeOffset From { get; private set; }
        public int RoomCapacity { get; private set; }
    }
}