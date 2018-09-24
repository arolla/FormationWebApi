using Bookings.Core;

namespace Bookings.Hosting.Models
{
    /// <summary>
    /// a booking patch
    /// </summary>
    public class BookingPatch
    {
        /// <summary>
        /// The new comment
        /// </summary>
        public string Comment { get; set; }

        internal BookingUpdate MapFor(int bookingId)
        {
            return new BookingUpdate(bookingId, Comment);
        }
    }
}