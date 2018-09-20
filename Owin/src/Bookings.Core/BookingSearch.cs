using System;

namespace Bookings.Core
{
    public class BookingSearch
    {
        public BookingSearch(DateTimeOffset from, DateTimeOffset to)
        {
            From = from;
            To = to;
        }

        public DateTimeOffset From { get; }
        public DateTimeOffset To { get; }
    }
}