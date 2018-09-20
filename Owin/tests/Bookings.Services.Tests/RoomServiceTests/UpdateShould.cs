using System;
using System.Collections.Generic;
using System.Text;
using Bookings.Core;
using NSubstitute;
using NUnit.Framework;

namespace Bookings.Services.Tests.RoomServiceTests
{
   public class UpdateShould : TestBase
    {
        [Test]
        public void Return_Updated_when_succeed(
            [Random(10)] int bookingId)
        {
            string comment =
                TestContext.CurrentContext.Random.GetString();

            var storedBooking = new StoredBooking(bookingId, 1, "Michel", "", DateTimeOffset.Now.AddDays(-2), DateTimeOffset.Now, 1);
            bookingStore.Get(Arg.Any<int>()).Returns(storedBooking);

            bookingStore
                .Update(bookingId, comment)
                .Returns(
                    new StoredBooking
                    (
                        bookingId,
                        1,
                        "Michel",
                        comment,
                        DateTimeOffset.Now.AddDays(-2),
                        DateTimeOffset.Now,
                        1
                    ));

            var bookingUpdate = new BookingUpdate(bookingId, comment);
            var bookingResult = roomService.Update(bookingUpdate);

            Assert.IsInstanceOf<BookingUpdateResult.Updated>(bookingResult);
        }

        [Test]
        public void Return_UpdateIsNull_when_bookingUpdate_is_null()
        {
            var bookingResult = roomService.Update(null);

            Assert.IsInstanceOf<BookingUpdateResult.BookingUpdateIsNull>(bookingResult);
        }

        [Test]
        public void Return_not_found_when_no_booking_found([Random(10)] int bookingId)
        {
            string comment =
                TestContext.CurrentContext.Random.GetString();

            bookingStore.Get(Arg.Any<int>()).Returns((StoredBooking)null);
            var bookingUpdate = new BookingUpdate(bookingId, comment);
            var bookingResult = roomService.Update(bookingUpdate);

            Assert.IsInstanceOf<BookingUpdateResult.NotFound>(bookingResult);
        }
    }
}
