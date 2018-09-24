﻿using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Bookings.Hosting.Tests
{
    public class BaseControllerTest
    {
        private TestServer server;
        protected HttpClient HttpClient { get; private set; }

        [SetUp]
        public void Setup()
        {
            this.server = new TestServer(
                new WebHostBuilder()
                .ConfigureTestServices(ConfigureService)
                .UseStartup<TestsStartup>());
            this.HttpClient = this.server.CreateClient();
        }

        protected virtual void ConfigureService(IServiceCollection services)
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