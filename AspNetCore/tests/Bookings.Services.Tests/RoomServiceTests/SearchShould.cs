using System;
using System.Collections.Generic;
using System.Text;
using Bookings.Core;
using NSubstitute;
using NUnit.Framework;

namespace Bookings.Services.Tests.RoomServiceTests
{
    public class SearchShould : TestBase
    {
        [Test]
        public void Return_Succeed_when_found()
        {
            bookingStore
                .Find(Arg.Any<DateTimeOffset>(), Arg.Any<DateTimeOffset>(), Arg.Any<int?>())
                .Returns(x => new StoredBooking[0]);

            var bookingSearch = new BookingSearch(DateTimeOffset.Now, DateTimeOffset.Now.AddDays(2));
            var bookingResult = roomService.Search(bookingSearch);

            Assert.IsInstanceOf<BookingSearchResult.Succeed>(bookingResult);
        }

        [Test]
        public void Return_error_when_BookingSearch_is_null()
        {
            bookingStore
                .Find(Arg.Any<DateTimeOffset>(), Arg.Any<DateTimeOffset>(), Arg.Any<int?>())
                .Returns(new StoredBooking[0]);

            var bookingResult = roomService.Search(null);

            Assert.IsInstanceOf<BookingSearchResult.BookingSearchIsNull>(bookingResult);
        }

        [Test]
        public void Return_error_when_error_occurred()
        {
            bookingStore.Find(Arg.Any<DateTimeOffset>(), Arg.Any<DateTimeOffset>(), Arg.Any<int?>()).Returns(x => throw new InvalidOperationException());

            var bookingSearch = new BookingSearch(DateTimeOffset.Now, DateTimeOffset.Now.AddDays(2));
            var bookingResult = roomService.Search(bookingSearch);

            Assert.IsInstanceOf<BookingSearchResult.Error>(bookingResult);
        }
    }

}
