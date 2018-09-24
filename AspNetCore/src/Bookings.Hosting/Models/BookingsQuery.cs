using System;
using System.ComponentModel.DataAnnotations;
using Bookings.Core;

namespace Bookings.Hosting.Models
{
    /// <summary>
    /// the availabilities filter
    /// </summary>
    public class BookingsQuery
    {
        /// <summary>
        /// the start of the period
        /// </summary>
        [Required]
        public DateTimeOffset? From { get; set; }
        /// <summary>
        /// the end of the period
        /// </summary>
        [Required]
        public DateTimeOffset? To { get; set; }

        internal BookingSearch Map()
        {
            return new BookingSearch(this.From.Value, this.To.Value);
        }
    }
}