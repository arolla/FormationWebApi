using System;
using Bookings.Core;
using NSubstitute;
using NUnit.Framework;

namespace Bookings.Services.Tests.RoomServiceTests
{
    public class GetBookingShould : TestBase
    {
        [Test]
        public void Return_Succeed_and_booking_when_found([Random(10)] int bookingId)
        {
            var storedBooking = new StoredBooking(bookingId, 1, "Michel", "Tres bien", DateTimeOffset.Now.AddDays(-2), DateTimeOffset.Now, 1);
            bookingStore.Get(Arg.Any<int>()).Returns(storedBooking);

            var bookingResult = roomService.Get(bookingId);

            Assert.IsInstanceOf<BookingResult.Succeed>(bookingResult);

            var booking = ((BookingResult.Succeed) bookingResult).Booking;

            Assert.AreEqual(
                new Booking(storedBooking.Id, storedBooking.RoomId, storedBooking.By, storedBooking.Comment, storedBooking.From, storedBooking.To, 1), booking);
        }

        [Test]
        public void Return_not_found_when_no_booking_found([Random(10)] int bookingId)
        {
            bookingStore.Get(Arg.Any<int>()).Returns((StoredBooking)null);

            var bookingResult = roomService.Get(bookingId);

            Assert.IsInstanceOf<BookingResult.NotFound>(bookingResult);
        }

        [Test]
        public void Return_error_when_error_occurred([Random(10)] int bookingId)
        {
            bookingStore.Get(Arg.Any<int>()).Returns(x => throw new InvalidOperationException());

            var bookingResult = roomService.Get(bookingId);

            Assert.IsInstanceOf<BookingResult.Error>(bookingResult);
        }
    }
}