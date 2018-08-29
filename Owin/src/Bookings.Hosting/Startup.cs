using System.Web.Http;
using Owin;

using Bookings.Hosting.Configurations;

namespace Bookings.Hosting
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration config = new HttpConfiguration(); 
            config.MapHttpAttributeRoutes();
            config.ConfigureSwagger();
            appBuilder.UseWebApi(config); 
        }
    }
}
