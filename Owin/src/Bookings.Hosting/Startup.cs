using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Bookings.Core;
using Owin;

using Bookings.Hosting.Configurations;
using Bookings.Hosting.Handlers;
using Bookings.Hosting.Logging;
using Bookings.Services;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

namespace Bookings.Hosting
{
    public class Startup
    {
        private Container container;

        public Startup()
        {
            this.container = new Container();
        }
        public void Configuration(IAppBuilder appBuilder)
        {
            this.ConfigureDependencies();
            HttpConfiguration config = new HttpConfiguration();

            config.Services.Replace(typeof(IExceptionHandler), new OopsExceptionHandler());
            config.Services.Add(typeof(IExceptionLogger), new FileExceptionLogger("log.txt"));

            config.DependencyResolver= new SimpleInjectorWebApiDependencyResolver(container);
            config.MapHttpAttributeRoutes();
            config.ConfigureSwagger();
            appBuilder.UseWebApi(config); 
        }

        private void ConfigureDependencies()
        {
            this.container.RegisterSingleton<RoomService>();
            this.container.Register<IAvailabilityService>(() => this.container.GetInstance<RoomService>());
            this.container.Register<IBookingService>(() => this.container.GetInstance<RoomService>());

            this.container.RegisterSingleton<IRoomStore, InMemoryRoomStore>();
            this.container.RegisterSingleton<IBookingStore, InMemoryBookingStore>();
        }
    }
}
