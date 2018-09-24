using System;
using System.Collections.Generic;
using Bookings.Hosting.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Bookings.Hosting.Examples
{
    public class BookingsViewExample : IExamplesProvider<BookingsView>
    {
        public BookingsView GetExamples()
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