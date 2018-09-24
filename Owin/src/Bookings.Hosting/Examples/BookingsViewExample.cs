using System;
using System.Collections.Generic;
using Bookings.Hosting.Models;
using Swashbuckle.Examples;

namespace Bookings.Hosting.Examples
{
    public class BookingsViewExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new BookingsView(GetBookingExamples());

        }

        private IEnumerable<BookingView> GetBookingExamples()
        {
            yield return new BookingView(1, DateTimeOffset.Parse("2018-01-01"), DateTimeOffset.Parse("2018-01-03"), 1, 50);
            yield return new BookingView(2, DateTimeOffset.Parse("2018-01-03"), DateTimeOffset.Parse("2018-01-10"), 1, 175);
            yield return new BookingView(2, DateTimeOffset.Parse("2018-01-01"), DateTimeOffset.Parse("2018-01-10"), 2, 423);
        }
    }
}