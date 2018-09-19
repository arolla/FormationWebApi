using System.Collections.Generic;

namespace Bookings.Hosting.Models
{
    /// <summary>
    /// The result of availabilities query
    /// </summary>
    public class AvailabilitiesView
    {
        public AvailabilitiesView(IEnumerable<Availability> availabilities)
        {
            Availabilities = availabilities;
        }

        /// <summary>
        /// the availabilities
        /// </summary>
        public IEnumerable<Availability> Availabilities { get; }
    }
}