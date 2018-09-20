using System;
using System.Collections.Generic;

namespace Bookings.Core
{
    public class Booking : IEquatable<Booking>
    {
        public Booking(int id, int roomId, string @by, string comment, DateTimeOffset @from, DateTimeOffset to, int price)
        {
            RoomId = roomId;
            By = @by;
            Comment = comment;
            From = @from;
            To = to;
            Price = price;
            Id = id;
        }

        public int Id { get; }
        public int RoomId { get; }
        public string By { get; }
        public string Comment { get; }
        public DateTimeOffset From { get; }
        public DateTimeOffset To { get; }
        public int Price { get;  }

        public override bool Equals(object obj)
        {
            return Equals(obj as Booking);
        }

        public bool Equals(Booking other)
        {
            return other != null &&
                   Id == other.Id &&
                   RoomId == other.RoomId &&
                   By == other.By &&
                   Comment == other.Comment &&
                   From.Equals(other.From) &&
                   To.Equals(other.To);
        }

        public override int GetHashCode()
        {
            var hashCode = -1563250390;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + RoomId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(By);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Comment);
            hashCode = hashCode * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(From);
            hashCode = hashCode * -1521134295 + EqualityComparer<DateTimeOffset>.Default.GetHashCode(To);
            return hashCode;
        }

        public static bool operator ==(Booking booking1, Booking booking2)
        {
            return EqualityComparer<Booking>.Default.Equals(booking1, booking2);
        }

        public static bool operator !=(Booking booking1, Booking booking2)
        {
            return !(booking1 == booking2);
        }
    }
}