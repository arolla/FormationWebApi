using System;
using System.Net;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using static Bookings.Core.BookingCancelResult;

namespace Bookings.Hosting.Tests.Controllers.BookingsControllerTests
{
    public class CancelShould : BookingControllerTests
    {
        [Test]
        public async Task Return_NoContent_when_the_booking_exist()
        {
            var bookingId = 1;
            this.BookingService
                .Cancel(bookingId)
                .Returns(Cancelled.Create(bookingId));
            var response = await HttpClient.DeleteAsync($"api/v1/bookings/{bookingId}");
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task Return_InternalServerError_when_something_went_wrong()
        {
            var bookingId = 3;
            this.BookingService
                .Cancel(bookingId)
                .Returns(Error.Create(new InvalidOperationException("Error system")));
            var response = await HttpClient.DeleteAsync($"api/v1/bookings/{bookingId}");
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Test]
        public async Task Return_NotFound_when_booking_does_not_exist()
        {
            var bookingId = 2;
            this.BookingService
                .Cancel(bookingId)
                .Returns(NotFound.Create(bookingId));
            var response = await HttpClient.DeleteAsync($"api/v1/bookings/{bookingId}");
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task Return_Unauthorized_when_user_is_not_logged()
        {
            UserContext.NotLogged();
            
            var bookingId = 1;
            var response = await HttpClient.DeleteAsync($"api/v1/bookings/{bookingId}");

            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            this.BookingService
                .DidNotReceiveWithAnyArgs()
                .Cancel(Arg.Any<int>());
        }

        [Test]
        public async Task Return_Unauthorized_when_user_is_logged_but_does_not_has_the_global_scope()
        {
            UserContext.LoggedWithScopes("fakeScope");

            var bookingId = 1;
            var response = await HttpClient.DeleteAsync($"api/v1/bookings/{bookingId}");

            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
            this.BookingService
                .DidNotReceiveWithAnyArgs()
                .Cancel(Arg.Any<int>());
        }
    }
}