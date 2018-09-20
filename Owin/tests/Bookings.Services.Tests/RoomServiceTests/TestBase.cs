using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using NUnit.Framework;

namespace Bookings.Services.Tests.RoomServiceTests
{
    public class TestBase
    {
        protected IBookingStore bookingStore;
        protected IRoomStore roomStore;
        protected RoomService roomService;

        [SetUp]
        public void Setup()
        {
            bookingStore = Substitute.For<IBookingStore>();
            roomStore = Substitute.For<IRoomStore>();
            roomService = new RoomService(roomStore, bookingStore);
        }
    }
}
