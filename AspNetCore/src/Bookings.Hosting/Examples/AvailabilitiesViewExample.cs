using System.Collections.Generic;
using Bookings.Hosting.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Bookings.Hosting.Examples
{
    public class AvailabilitiesViewExample : IExamplesProvider<AvailabilitiesView>
    {
        public AvailabilitiesView GetExamples()
        {
            return new AvailabilitiesView(GetAvailabilities());
        }

        private IEnumerable<AvailabilityView> GetAvailabilities()
        {
            yield return new AvailabilityView(1, 2, 30);
            yield return new AvailabilityView(2, 1, 25);
            yield return new AvailabilityView(3, 1, 100);
        }
    }
}