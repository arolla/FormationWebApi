using System.Collections.Generic;

namespace Bookings.Hosting.Models
{
    /// <summary>
    /// The result of bookings query
    /// </summary>
    public class BookingsView
    {
        public BookingsView(IEnumerable<BookingView> bookings)
        {
            Bookings = bookings;
        }

        /// <summary>
        /// the bookings
        /// </summary>
        public IEnumerable<BookingView> Bookings { get; }
    }
}