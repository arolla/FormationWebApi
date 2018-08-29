using System;

namespace Bookings.Hosting.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int RoomId { get; set; }
        public int TotalPrice { get; set; }
    }
}