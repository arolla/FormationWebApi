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
        public async Task Return_Ok_when_valid_request()
        {
            using (var server = new TestServer(new WebHostBuilder().UseStartup<TestsStartup>()))
            using (var client = server.CreateClient())
            {
                var response = await client.GetAsync("api/v1/availabilities?from=2018-01-01&to=2018-01-02");
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }
    }
}