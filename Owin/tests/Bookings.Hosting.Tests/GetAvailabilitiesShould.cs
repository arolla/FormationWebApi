using Microsoft.Owin.Testing;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

namespace Bookings.Hosting.Tests
{
    public class GetAvailabilitiesShould
    {
        [Test]
        public async Task Return_Ok_when_valid_request()
        {
            using (var server = TestServer.Create<TestsStartup>())
            using (var client = server.HttpClient)
            {
                var response = await client.GetAsync("api/v1/availabilities?from=2018-01-01&to=2018-01-02");
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }
    }
}