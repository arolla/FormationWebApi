using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using Bookings.Core;
using Bookings.Hosting.Models;
using NSubstitute;
using NUnit.Framework;
using static Bookings.Core.BookingCreationResult;

namespace Bookings.Hosting.Tests.Controllers.BookingsControllerTests
{
    public class BookShould : BookingControllerTests
    {
        private BookingInput ValidInput => new BookingInput { From = DateTimeOffset.Now, To = DateTimeOffset.Now.AddDays(2), RoomId = 1, By = "jean@sansmail.com", Comment = "Need upgrade" };

        [Test]
        public async Task Return_Created_when_a_valid_booking_is_send()
        {
            var input = ValidInput;
           
            this.BookingService
                .Book(Arg.Is<BookingCreation>(a => a.RoomId == input.RoomId && a.From == input.From && a.To == input.To && a.By == input.By))
                .Returns(Created.Create(new Booking(1, input.RoomId.Value, input.By, input.Comment, input.From.Value, input.To.Value, 1)));

            var response = await HttpClient.PostAsync("api/v1/bookings", new ObjectContent<BookingInput>(input, new JsonMediaTypeFormatter()));
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public async Task Return_bad_request_when_nothing_is_send()
        {
            var response = await this.HttpClient.PostAsync("api/v1/bookings", new StringContent("{}", Encoding.UTF8, "application/json"));
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task Return_bad_request_when_from_date_is_missing()
        {
            var input = ValidInput;
            input.From = null;
            var response = await this.HttpClient.PostAsync("api/v1/bookings", new ObjectContent<BookingInput>(input, new JsonMediaTypeFormatter()));
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task Return_InternalServerError_when_something_went_wrong()
        {
            var input = ValidInput;
            input.RoomId = 3;
            this.BookingService
                .Book(Arg.Is<BookingCreation>(a => a.RoomId == input.RoomId))
                .Returns(Error.Create(new InvalidOperationException("Error system")));
            var response = await HttpClient.PostAsync("api/v1/bookings", new ObjectContent<BookingInput>(input, new JsonMediaTypeFormatter()));
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Test]
        public async Task Return_Conflict_when_room_is_already_booked_at_this_time()
        {
            var input = ValidInput;
            input.RoomId = 2;

            this.BookingService
                .Book(Arg.Is<BookingCreation>(a => a.RoomId == input.RoomId))
                .Returns(Conflict.Create());
            var response = await HttpClient.PostAsync("api/v1/bookings", new ObjectContent<BookingInput>(input, new JsonMediaTypeFormatter()));
            Assert.AreEqual(HttpStatusCode.Conflict, response.StatusCode);
        }

        [Test]
        public async Task Return_Unauthorized_when_user_is_not_logged()
        {
            UserContext.NotLogged();
            
            var response = await HttpClient.PostAsync("api/v1/bookings", new ObjectContent<BookingInput>(ValidInput, new JsonMediaTypeFormatter()));

            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            this.BookingService
                .DidNotReceiveWithAnyArgs()
                .Book(Arg.Any<BookingCreation>());
        }

        [Test]
        public async Task Return_Unauthorized_when_user_is_logged_but_does_not_has_the_global_scope()
        {
            UserContext.LoggedWithScopes("fakeScope");

            var response = await HttpClient.PostAsync("api/v1/bookings", new ObjectContent<BookingInput>(ValidInput, new JsonMediaTypeFormatter()));

            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            this.BookingService
                .DidNotReceiveWithAnyArgs()
                .Book(Arg.Any<BookingCreation>());
        }
    }
}