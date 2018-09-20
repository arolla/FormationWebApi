using System;

namespace Bookings.Services
{
    public class StoredBooking
    {
        public int Id { get; }
        public int RoomId { get; }
        public string By { get; }
        public string Comment { get; }
        public DateTimeOffset From { get; }
        public DateTimeOffset To { get; }
        public int Price { get; }

        public StoredBooking(int id, int roomId, string @by, string comment, DateTimeOffset @from, DateTimeOffset to, int price)
        {
            Id = id;
            RoomId = roomId;
            By = @by;
            Comment = comment;
            From = @from;
            To = to;
            Price = price;
        }
    }
}