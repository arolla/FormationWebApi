using System;
using System.ComponentModel.DataAnnotations;
using Bookings.Hosting.Validators;

namespace Bookings.Hosting.Models
{
    /// <summary>
    /// The booking creation model
    /// </summary>
    public class BookingInput
    {
        /// <summary>
        /// The date of check in (yyyy-MM-dd)
        /// </summary>
        [Required]
        [NotBeforeToday]
        public DateTimeOffset? From { get; set; }

        /// <summary>
        /// The date of checkout (yyyy-MM-dd)
        /// </summary>
        [Required]
        public DateTimeOffset? To { get; set; }

        /// <summary>
        /// The id of the room
        /// </summary>
        [Required]
        public int RoomId { get; set; }

        /// <summary>
        /// the customer email
        /// </summary>
        [Required]
        public string By { get; set; }

        /// <summary>
        /// the comment for the booking
        /// </summary>
        public string Comment { get; set; }
    }
}