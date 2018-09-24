using System;
using System.Net;
using System.Threading.Tasks;
using Bookings.Core;
using NSubstitute;
using NUnit.Framework;
using static Bookings.Core.BookingResult;

namespace Bookings.Hosting.Tests.Controllers.BookingsControllerTests
{
    public class GetByIdShould : BookingControllerTests
    {
        [Test]
        public async Task Return_Ok_when_looked_booking_exist()
        {
            var bookingId = 1;
            this.BookingService
                .Get(bookingId)
                .Returns(Succeed.Create(new Booking(bookingId, 1, "jean@sansmail.com", "", DateTimeOffset.Now, DateTimeOffset.Now.AddDays(15), 1)));
            var response = await HttpClient.GetAsync($"api/v1/bookings/{bookingId}");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task Return_NotFound_when_booking_does_not_exist()
        {
            var bookingId = 2;
            this.BookingService
                .Get(bookingId)
                .Returns(NotFound.Create(bookingId));
            var response = await HttpClient.GetAsync($"api/v1/bookings/{bookingId}");
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task Return_InternalServerError_when_something_went_wrong()
        {
            var bookingId = 3;
            this.BookingService
                .Get(bookingId)
                .Returns(Error.Create(new InvalidOperationException("Error system")));
            var response = await HttpClient.GetAsync($"api/v1/bookings/{bookingId}");
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Test]
        public async Task Return_Unauthorized_when_user_is_not_logged()
        {
            UserContext.NotLogged();
            
            var bookingId = 1;

            var response = await HttpClient.GetAsync($"api/v1/bookings/{bookingId}");

            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            this.BookingService
                .DidNotReceiveWithAnyArgs()
                .Get(Arg.Any<int>());
        }

        [Test]
        public async Task Return_Unauthorized_when_user_is_logged_but_does_not_has_the_global_scope()
        {
            UserContext.LoggedWithScopes("fakeScope");

            var bookingId = 1;

            var response = await HttpClient.GetAsync($"api/v1/bookings/{bookingId}");

            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            this.BookingService
                .DidNotReceiveWithAnyArgs()
                .Get(Arg.Any<int>());
        }
    }
}