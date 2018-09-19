using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Owin;

namespace Bookings.Hosting.Tests
{
    public class TestsStartup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();

            config.Services.Replace(typeof(IAssembliesResolver), new TestWebApiResolver());

            appBuilder.UseWebApi(config);
        }
    }

    class TestWebApiResolver : IAssembliesResolver
    {
        public ICollection<Assembly> GetAssemblies()
        {
            return new Assembly[]
            {
                Assembly.GetAssembly(typeof(Bookings.Hosting.Startup))
            };
        }
    }
}