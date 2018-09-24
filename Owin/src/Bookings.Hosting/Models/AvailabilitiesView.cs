using System.Collections.Generic;
using Bookings.Core;

namespace Bookings.Hosting.Models
{
    /// <summary>
    /// The result of availabilities query
    /// </summary>
    public class AvailabilitiesView
    {
        public AvailabilitiesView(IEnumerable<AvailabilityView> availabilities)
        {
            Availabilities = availabilities;
        }

        /// <summary>
        /// the availabilities
        /// </summary>
        public IEnumerable<AvailabilityView> Availabilities { get; }
    }
}