using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Bookings.Core;
using Bookings.Hosting.Models;
using NSubstitute;
using NUnit.Framework;
using static Bookings.Core.BookingUpdateResult;

namespace Bookings.Hosting.Tests.Controllers.BookingsControllerTests
{
    public class UpdateShould : BookingControllerTests
    {
        private BookingPatch ValidInput => new BookingPatch { Comment = "Need upgrade" };

        private Task<HttpResponseMessage> PatchAsync(string uri, HttpContent content)
        {
            return this.HttpClient.SendAsync(new HttpRequestMessage(new HttpMethod("PATCH"), uri) {Content = content});
        }

        [Test]
        public async Task Return_Ok_when_the_booking_exist()
        {
            var bookingId = 1;
            this.BookingService
                .Update(Arg.Is<BookingUpdate>(a => a.BookingId == bookingId))
                .Returns(Updated.Create(new Booking(bookingId, 1, "jean@sansmail.com", ValidInput.Comment, DateTimeOffset.Now, DateTimeOffset.Now.AddDays(1), 50)));
            var response = await this.PatchAsync($"api/v1/bookings/{bookingId}", new JsonContent(ValidInput));
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task Return_InternalServerError_when_something_went_wrong()
        {
            var bookingId = 3;
            this.BookingService
                .Update(Arg.Is<BookingUpdate>(a => a.BookingId == bookingId))
                .Returns(Error.Create(new InvalidOperationException("Error system")));
            var response = await this.PatchAsync($"api/v1/bookings/{bookingId}", new JsonContent(ValidInput));
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Test]
        public async Task Return_NotFound_when_booking_does_not_exist()
        {
            var bookingId = 2;

            this.BookingService
                .Update(Arg.Is<BookingUpdate>(a => a.BookingId == bookingId))
                .Returns(NotFound.Create(bookingId));
            var response = await this.PatchAsync($"api/v1/bookings/{bookingId}", new JsonContent(ValidInput));
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task Return_Unauthorized_when_user_is_not_logged()
        {
            UserContext.NotLogged();
            var bookingId = 1;

            var response = await this.PatchAsync($"api/v1/bookings/{bookingId}", new JsonContent(ValidInput));
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            this.BookingService
                .DidNotReceiveWithAnyArgs()
                .Update(Arg.Any<BookingUpdate>());
        }

        [Test]
        public async Task Return_Unauthorized_when_user_is_logged_but_does_not_has_the_global_scope()
        {
            UserContext.LoggedWithScopes("fakeScope");
            var bookingId = 1;

            var response = await this.PatchAsync($"api/v1/bookings/{bookingId}", new JsonContent(ValidInput));
            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
            this.BookingService
                .DidNotReceiveWithAnyArgs()
                .Update(Arg.Any<BookingUpdate>());
        }
    }
}