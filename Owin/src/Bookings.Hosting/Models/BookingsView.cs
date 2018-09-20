using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Bookings.Hosting.Validators;

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

    /// <summary>
    /// the availabilities filter
    /// </summary>
    public class BookingsQuery
    {
        /// <summary>
        /// the start of the period
        /// </summary>
        [Required]
        [NotBeforeToday]
        public DateTimeOffset? From { get; set; }
        /// <summary>
        /// the end of the period
        /// </summary>
        [Required]
        public DateTimeOffset? To { get; set; }
    }
}