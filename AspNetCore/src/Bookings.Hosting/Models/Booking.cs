using System;

namespace Bookings.Hosting.Models
{
    /// <summary>
    /// Represent a booking
    /// </summary>
    public class Booking
    {
        /// <summary>
        /// The id of the booking
        /// </summary>
        public int BookingId { get; set; }
        /// <summary>
        /// The starting date of the booking period
        /// </summary>
        public DateTime From { get; set; }
        /// <summary>
        /// The ending date of the booking period
        /// </summary>
        public DateTime To { get; set; }
        /// <summary>
        /// The id of the room
        /// </summary>
        public int RoomId { get; set; }
        /// <summary>
        /// The total price for this booking
        /// </summary>
        public int TotalPrice { get; set; }
    }
}