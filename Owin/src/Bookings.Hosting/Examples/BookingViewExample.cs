﻿using System;
using Bookings.Hosting.Models;
using Swashbuckle.Examples;

namespace Bookings.Hosting.Examples
{
    public class BookingViewExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new BookingView(1, DateTimeOffset.Parse("2018-01-01"), DateTimeOffset.Parse("2018-01-03"), 1, 50);
        }
    }
}