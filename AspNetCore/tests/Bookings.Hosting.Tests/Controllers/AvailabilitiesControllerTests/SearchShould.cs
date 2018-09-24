using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Bookings.Core;
using Bookings.Hosting.Models;
using Bookings.Services;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NSubstitute;
using NSubstitute.ClearExtensions;
using NUnit.Framework;

namespace Bookings.Hosting.Tests.Controllers.AvailabilitiesControllerTests
{
    public class SearchShould : BaseControllerTest
    {
        private readonly IAvailabilityService availabilityService = Substitute.For<IAvailabilityService>();

        protected override void ConfigureService(IServiceCollection services)
        {
            availabilityService.ClearSubstitute();
            services.AddSingleton(availabilityService);
        }

        [Test]
        public async Task Return_Ok_when_period_is_valid()
        {
            var from = DateTimeOffset.Now;
            var to = DateTimeOffset.Now.AddDays(1);
            this.availabilityService
                .GetAvailabilities(Arg.Is<AvailabilitySearch>(a => a.From == from.Date && a.To == to.Date))
                .Returns(AvailabilitiesResult.Succeed.Create(new []{ new Availability(new Room(1, 1, 50),from.Date, to.Date, 50)  }));

            var response = await this.HttpClient.GetAsync($"api/v1/availabilities?from={from:yyyy-MM-dd}&to={to:yyyy-MM-dd}");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            var availabilities = JsonConvert.DeserializeObject<AvailabilitiesView>(content);
            Assert.AreEqual(1, availabilities.Availabilities.Count());
        }

        [Test]
        public async Task Return_Ok_when_no_period_filled()
        {
            var response = await this.HttpClient.GetAsync($"api/v1/availabilities");
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task Return_BadRequest_when_the_period_does_not_cover_at_least_one_night()
        {
            var from = DateTimeOffset.Now;
            var to = from;
            var response = await this.HttpClient.GetAsync($"api/v1/availabilities?from={from:yyyy-MM-dd}&to={to:yyyy-MM-dd}");
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task Return_BadRequest_when_the_period_start_is_greater_than_period_end()
        {
            var from = DateTimeOffset.Now.AddDays(15);
            var to = DateTimeOffset.Now;
            var response = await this.HttpClient.GetAsync($"api/v1/availabilities?from={from:yyyy-MM-dd}&to={to:yyyy-MM-dd}");
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task Return_BadRequest_when_the_period_start_before_today()
        {
            var from = DateTimeOffset.Now.AddDays(-1);
            var to = DateTimeOffset.Now.AddDays(1);
            var response = await this.HttpClient.GetAsync($"api/v1/availabilities?from={from:yyyy-MM-dd}&to={to:yyyy-MM-dd}");
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public async Task Return_Unauthorized_when_user_is_not_logged()
        {
            UserContext.NotLogged();

            var from = DateTimeOffset.Now;
            var to = DateTimeOffset.Now.AddDays(1);

            var response = await this.HttpClient.GetAsync($"api/v1/availabilities?from={from:yyyy-MM-dd}&to={to:yyyy-MM-dd}");
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            this.availabilityService
                .DidNotReceiveWithAnyArgs()
                .GetAvailabilities(Arg.Any<AvailabilitySearch>());
        }

        [Test]
        public async Task Return_Forbidden_when_user_is_logged_but_does_not_has_the_global_scope()
        {
            UserContext.LoggedWithScopes("fakeScope");

            var from = DateTimeOffset.Now;
            var to = DateTimeOffset.Now.AddDays(1);

            var response = await this.HttpClient.GetAsync($"api/v1/availabilities?from={from:yyyy-MM-dd}&to={to:yyyy-MM-dd}");
            Assert.AreEqual(HttpStatusCode.Forbidden, response.StatusCode);
            this.availabilityService
                .DidNotReceiveWithAnyArgs()
                .GetAvailabilities(Arg.Any<AvailabilitySearch>());
        }
    }
}