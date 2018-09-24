using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Bookings.Core;
using Bookings.Hosting.Models;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using static Bookings.Core.BookingSearchResult;

namespace Bookings.Hosting.Tests.Controllers.BookingsControllerTests
{
    public class SearchShould : BookingControllerTests
    {
        [Test]
        public async Task Return_Ok_when_period_is_valid()
        {
            var from = "2018-01-02";
            var to =  "2018-01-04";

            this.BookingService
                .Search(Arg.Is<BookingSearch>(a => a.From == DateTimeOffset.Parse(from) && a.To == DateTimeOffset.Parse(to)))
                .Returns(Succeed.Create (new []{ new Booking(1, 1, "jean@pasdemail.com", null, DateTimeOffset.Parse(from), DateTimeOffset.Parse(to), 50) }));
           
            var response = await this.HttpClient.GetAsync($"api/v1/bookings?from={from}&to={to}");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            var bookings = JsonConvert.DeserializeObject<BookingsView>(content);
            Assert.AreEqual(1, bookings.Bookings.Count());
        }

        [Test]
        public async Task Return_Ok_when_no_period_filled()
        {
            var response = await this.HttpClient.GetAsync($"api/v1/bookings");
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task Return_BadRequest_when_the_period_start_is_greater_than_period_end()
        {
            var from = DateTimeOffset.Now.AddDays(15);
            var to = DateTimeOffset.Now;
            var response = await this.HttpClient.GetAsync($"api/v1/bookings?from={from:yyyy-MM-dd}&to={to:yyyy-MM-dd}");
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task Return_NotFound_when_booking_does_not_exist()
        {
            var from = "2018-01-02";
            var to =  "2018-01-04";
            this.BookingService
                .Search(Arg.Is<BookingSearch>(a => a.From == DateTimeOffset.Parse(from) && a.To == DateTimeOffset.Parse(to)))
                .Returns(Error.Create(new InvalidOperationException("Error system")));
            var response = await this.HttpClient.GetAsync($"api/v1/bookings?from={from:yyyy-MM-dd}&to={to:yyyy-MM-dd}");
            Assert.AreEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Test]
        public async Task Return_Unauthorized_when_user_is_not_logged()
        {
            UserContext.NotLogged();
            
            var from = "2018-01-02";
            var to =  "2018-01-04";
           
            var response = await this.HttpClient.GetAsync($"api/v1/bookings?from={from}&to={to}");

            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            this.BookingService
                .DidNotReceiveWithAnyArgs()
                .Search(Arg.Any<BookingSearch>());
        }

        [Test]
        public async Task Return_Unauthorized_when_user_is_logged_but_does_not_has_the_global_scope()
        {
            UserContext.LoggedWithScopes("fakeScope");

            var from = "2018-01-02";
            var to =  "2018-01-04";
           
            var response = await this.HttpClient.GetAsync($"api/v1/bookings?from={from}&to={to}");
            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
            this.BookingService
                .DidNotReceiveWithAnyArgs()
                .Search(Arg.Any<BookingSearch>());
        }
    }
}