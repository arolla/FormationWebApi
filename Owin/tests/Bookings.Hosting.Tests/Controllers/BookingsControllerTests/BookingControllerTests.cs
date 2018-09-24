﻿using Bookings.Core;
using NSubstitute;
using SimpleInjector;

namespace Bookings.Hosting.Tests.Controllers.BookingsControllerTests
{
    public class BookingControllerTests : BaseControllerTest
    {
        protected readonly IBookingService BookingService = Substitute.For<IBookingService>();

        protected override void Configure(Container container)
        {
            container.RegisterInstance(BookingService);
        }
    }
}