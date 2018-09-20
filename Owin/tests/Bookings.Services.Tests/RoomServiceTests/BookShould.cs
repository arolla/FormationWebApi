using System;
using System.Collections.Generic;
using System.Text;
using Bookings.Core;
using NSubstitute;
using NUnit.Framework;

namespace Bookings.Services.Tests.RoomServiceTests
{
   public class BookShould : TestBase
    {
        [Test]
        public void Return_BookingCreationIsNull_when_bookingCreation_is_null()
        {
            var bookingResult = roomService.Book(null);

            Assert.IsInstanceOf<BookingCreationResult.BookingCreationIsNull>(bookingResult);
        }

        [Test]
        public void Return_Conflict_when_already_booked()
        {
            var stored =
                new []
                {
                    new StoredBooking(1, 1, "Michel", "Tres bien", DateTimeOffset.Now.AddDays(-2), DateTimeOffset.Now, 1)
                };

            bookingStore
                .Find(Arg.Any<DateTimeOffset>(), Arg.Any<DateTimeOffset>(), Arg.Any<int?>())
                .Returns(stored);

            var bookingCreation = new BookingCreation(stored[0].RoomId, stored[0].By, stored[0].Comment, stored[0].From, stored[0].To);
            var bookingResult = roomService.Book(bookingCreation);

            Assert.IsInstanceOf<BookingCreationResult.Conflict>(bookingResult);
        }

        [Test]
        public void Return_Created_when_succeed()
        {
            var stored =
                new StoredBooking[0];

            bookingStore
                .Find(Arg.Any<DateTimeOffset>(), Arg.Any<DateTimeOffset>(), Arg.Any<int?>())
                .Returns(stored);

            roomStore.GetRoom(1)
                .Returns(new StoredRoom(1, 1, 1));

            var bookingCreation = 
                new BookingCreation(
                    1,
                    "Michel",
                    "Tres bien",
                    DateTimeOffset.Now.AddDays(-2),
                    DateTimeOffset.Now);

            bookingStore
                .Create(bookingCreation.RoomId, bookingCreation.By, bookingCreation.Comment, bookingCreation.From, bookingCreation.To, 2)
                .Returns(new StoredBooking(1, bookingCreation.RoomId, bookingCreation.By, bookingCreation.Comment, bookingCreation.From, bookingCreation.To, 2));

            var bookingResult = roomService.Book(bookingCreation);

            Assert.IsInstanceOf<BookingCreationResult.Created>(bookingResult);
        }
    }
}
