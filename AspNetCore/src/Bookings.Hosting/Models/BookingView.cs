using System;
using Bookings.Core;

namespace Bookings.Hosting.Models
{
    /// <summary>
    /// Represent a booking
    /// </summary>
    public class BookingView
    {
        /// <summary>
        /// The id of the booking
        /// </summary>
        public int BookingId { get; }

        /// <summary>
        /// The date of check in (yyyy-MM-dd)
        /// </summary>
        public DateTimeOffset From { get; }

        /// <summary>
        /// The date of checkout (yyyy-MM-dd)
        /// </summary>
        public DateTimeOffset To { get; }

        /// <summary>
        /// The id of the room
        /// </summary>
        public int RoomId { get; }

        /// <summary>
        /// The price of the room (currency assumed)s
        /// </summary>
        public int Price { get; }

        public BookingView(int bookingId, DateTimeOffset from, DateTimeOffset to, int roomId, int price)
        {
            BookingId = bookingId;
            From = from;
            To = to;
            RoomId = roomId;
            Price = price;
        }

        internal static BookingView For(Booking booking)
        {
            return new BookingView(booking.Id, booking.From, booking.To, booking.RoomId, booking.Price);
        }
    }
}