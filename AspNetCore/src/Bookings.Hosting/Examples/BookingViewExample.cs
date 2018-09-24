using System;
using Bookings.Hosting.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Bookings.Hosting.Examples
{
    public class BookingViewExample : IExamplesProvider<BookingView>
    {
        public BookingView GetExamples()
        {
            return new BookingView(1, DateTimeOffset.Parse("2018-01-01"), DateTimeOffset.Parse("2018-01-03"), 1, 50);
        }
    }
}