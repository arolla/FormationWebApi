using System;

namespace Bookings.Core
{
    public class BookingCreation
    {
        public BookingCreation(int roomId, string by, string comment, DateTimeOffset from, DateTimeOffset to)
        {
            RoomId = roomId;
            By = by;
            Comment = comment;
            From = from;
            To = to;
        }

        public int RoomId { get; }
        public string By { get; }
        public string Comment { get; }
        public DateTimeOffset From { get; }
        public DateTimeOffset To { get; }
    }
}