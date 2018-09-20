using System;
using System.Collections.Generic;
using System.Text;
using Bookings.Core;
using NSubstitute;
using NUnit.Framework;

namespace Bookings.Services.Tests.RoomServiceTests
{
   public class GetAvailabilitiesShould : TestBase
    {
        [Test]
        public void Return_BookingCreationIsNull_when_bookingCreation_is_null()
        {
            var availabilitiesResult = roomService.GetAvailabilities(null);

            Assert.IsInstanceOf<AvailabilitiesResult.AvailabilitySearchIsNull>(availabilitiesResult);
        }

        [Test]
        public void Return_Succeed_when_valid_search()
        {
            var availabilitySearch =
                new AvailabilitySearch(DateTimeOffset.Now.AddDays(-2), DateTimeOffset.Now, 1);

            var availabilitiesResult = roomService.GetAvailabilities(availabilitySearch);

            Assert.IsInstanceOf<AvailabilitiesResult.Succeed>(availabilitiesResult);
        }

        [Test]
        public void Return_Error_when_error_occurred()
        {
            bookingStore.GetPeriodBookings(Arg.Any<DateTimeOffset>(), Arg.Any<DateTimeOffset>())
                .Returns(x => throw new InvalidOperationException());

            var availabilitySearch =
                new AvailabilitySearch(DateTimeOffset.Now.AddDays(-2), DateTimeOffset.Now, 1);
            var availabilitiesResult = roomService.GetAvailabilities(availabilitySearch);

            Assert.IsInstanceOf<AvailabilitiesResult.Error>(availabilitiesResult);
        }
    }
}
