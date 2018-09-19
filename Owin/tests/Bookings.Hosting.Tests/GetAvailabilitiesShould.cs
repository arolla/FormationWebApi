using Microsoft.Owin.Testing;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

namespace Bookings.Hosting.Tests
{
    public class GetAvailabilitiesShould
    {
        [Test]
        public async Task Return_Ok_when_period_is_valid()
        {
            using (var server = TestServer.Create<TestsStartup>())
            using (var client = server.HttpClient)
            {
                var response = await client.GetAsync("api/v1/availabilities?from=2018-01-01&to=2018-01-02");
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [TestCase("2018-01-01","2018-01-01")]
        [TestCase("2018-01-03","2018-01-01")]
        public async Task Return_BadRequest_when_the_period_does_not_cover_at_least_one_night(string from, string to)
        {
            using (var server = TestServer.Create<TestsStartup>())
            using (var client = server.HttpClient)
            {
                var response = await client.GetAsync($"api/v1/availabilities?from={from}&to={to}");
                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }
    }
}