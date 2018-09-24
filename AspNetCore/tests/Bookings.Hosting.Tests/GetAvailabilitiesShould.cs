using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;

namespace Bookings.Hosting.Tests
{
    public class GetAvailabilitiesShould
    {
        [Test]
        public async Task Return_Ok_when_period_is_valid()
        {
            using (var server = new TestServer(new WebHostBuilder().UseStartup<TestsStartup>()))
            using (var client = server.CreateClient())
            {
                var from = DateTimeOffset.Now;
                var to = DateTimeOffset.Now.AddDays(1);
                var response = await client.GetAsync($"api/v1/availabilities?from={from:yyyy-MM-dd}&to={to:yyyy-MM-dd}");
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Test]
        public async Task Return_BadRequest_when_the_period_does_not_cover_at_least_one_night()
        {
            using (var server = new TestServer(new WebHostBuilder().UseStartup<TestsStartup>()))
            using (var client = server.CreateClient())
            {
                var from = DateTimeOffset.Now;
                var to = from;
                var response = await client.GetAsync($"api/v1/availabilities?from={from:yyyy-MM-dd}&to={to:yyyy-MM-dd}");
                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Test]
        public async Task Return_BadRequest_when_the_period_start_is_greater_than_period_end()
        {
            using (var server = new TestServer(new WebHostBuilder().UseStartup<TestsStartup>()))
            using (var client = server.CreateClient())
            {
                var from = DateTimeOffset.Now.AddDays(15);
                var to = DateTimeOffset.Now;
                var response = await client.GetAsync($"api/v1/availabilities?from={from:yyyy-MM-dd}&to={to:yyyy-MM-dd}");
                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Test]
        public async Task Return_BadRequest_when_the_period_start_before_today()
        {
            using (var server = new TestServer(new WebHostBuilder().UseStartup<TestsStartup>()))
            using (var client = server.CreateClient())
            {
                var from = DateTimeOffset.Now.AddDays(-1);
                var to = DateTimeOffset.Now.AddDays(1);
                var response = await client.GetAsync($"api/v1/availabilities?from={from:yyyy-MM-dd}&to={to:yyyy-MM-dd}");
                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }
    }
}