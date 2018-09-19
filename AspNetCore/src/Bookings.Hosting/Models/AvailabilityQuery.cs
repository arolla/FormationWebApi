using System;
using System.ComponentModel.DataAnnotations;
using Bookings.Hosting.Validators;

namespace Bookings.Hosting.Models
{
    /// <summary>
    /// the availabilities filter
    /// </summary>
    public class AvailabilityQuery
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