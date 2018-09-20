using System;
using System.Collections.Generic;
using System.Text;
using Bookings.Core;
using NSubstitute;
using NUnit.Framework;

namespace Bookings.Services.Tests.RoomServiceTests
{
   public class CancelShould : TestBase
    {
        [Test]
        public void Return_Cancelled_when_succeed([Random(10)] int bookingId)
        {
            var storedBooking = new StoredBooking(bookingId, 1, "Michel", "Tres bien", DateTimeOffset.Now.AddDays(-2), DateTimeOffset.Now, 1);
            bookingStore.Get(Arg.Any<int>()).Returns(storedBooking);

            var bookingResult = roomService.Cancel(bookingId);

            bookingStore.Received(1).Delete(bookingId);
            Assert.IsInstanceOf<BookingCancelResult.Cancelled>(bookingResult);
        }

        [Test]
        public void Return_not_found_when_no_booking_found([Random(10)] int bookingId)
        {
            bookingStore.Get(Arg.Any<int>()).Returns((StoredBooking)null);

            var bookingResult = roomService.Cancel(bookingId);

            Assert.IsInstanceOf<BookingCancelResult.NotFound>(bookingResult);
        }

        [Test]
        public void Return_error_when_BookingSearch_error_occurred([Random(10)] int bookingId)
        {
            bookingStore.Get(Arg.Any<int>()).Returns(x => throw new InvalidOperationException());

            var bookingResult = roomService.Cancel(bookingId);

            Assert.IsInstanceOf<BookingCancelResult.Error>(bookingResult);
        }
    }
}
