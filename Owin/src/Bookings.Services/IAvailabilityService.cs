using System;
using System.Collections.Generic;
using System.Text;
using Bookings.Core;

namespace Bookings.Services
{
   public interface IAvailabilityService
   {
       IEnumerable<Availability> GetAvailabilities(AvailabilitySearch availabilitySearch);
   }
}
