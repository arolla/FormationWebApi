using System.Collections.Generic;
using Bookings.Services;

namespace Bookings.Core
{
   public interface IAvailabilityService
   {
       AvailabilitiesResult GetAvailabilities(AvailabilitySearch availabilitySearch);
   }
}
