﻿using System;
using System.ComponentModel.DataAnnotations;
using Bookings.Core;
using Bookings.Hosting.Validators;

namespace Bookings.Hosting.Models
{
    /// <summary>
    /// the availabilities filter
    /// </summary>
    public class AvailabilitiesQuery
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

        /// <summary>
        /// Specify minimal room capacity
        /// </summary>
        public int? RoomCapacity { get; set; }

        internal AvailabilitySearch Map()
        {
            return new AvailabilitySearch(From.Value, To.Value, RoomCapacity);
        }
    }
}