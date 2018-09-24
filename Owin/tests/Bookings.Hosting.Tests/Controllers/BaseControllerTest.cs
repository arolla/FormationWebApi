using System.Net.Http;
using Microsoft.Owin.Testing;
using NUnit.Framework;
using SimpleInjector;

namespace Bookings.Hosting.Tests.Controllers
{
    public class BaseControllerTest
    {
        private TestServer server;
        protected HttpClient HttpClient { get; private set; }

        [SetUp]
        public void Setup()
        {
            var container = new Container();
            Configure(container);
            var startup = new TestsStartup(container);
            this.server = TestServer.Create(startup.Configuration);
            this.HttpClient = this.server.HttpClient;
        }

        protected virtual void Configure(Container container)
        {
        }

        [TearDown]
        public void TearDown()
        {
            this.HttpClient.Dispose();
            this.HttpClient = null;
            this.server.Dispose();
            this.server = null;
        }
    }
}