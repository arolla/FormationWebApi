using System.Collections.Generic;
using System.Reflection;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Bookings.Hosting.Configurations;
using Owin;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

namespace Bookings.Hosting.Tests
{
    public class TestsStartup
    {
        private readonly Container container;
        private readonly TestUserContext userContext;

        public TestsStartup(Container container, TestUserContext userContext)
        {
            this.container = container;
            this.userContext = userContext;
        }

        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
            config.MapHttpAttributeRoutes();

            config.Services.Replace(typeof(IAssembliesResolver), new TestWebApiResolver());

            appBuilder.Use((context, next) =>
            {
                context.Authentication.User = userContext.Principal;
                return next();
            });
            appBuilder.ConfigureSecurity(config);
            appBuilder.UseWebApi(config);
        }
    }

    public class TestUserContext
    {
        internal ClaimsPrincipal Principal { get; private set; } 

        public void WithGlobalScope()
        {
            this.Principal = new ClaimsPrincipal(new ClaimsIdentity(new []
            {
                new Claim("scope", Scopes.ApiV1), 
            }));
        }

        public void NotLogged()
        {
            this.Principal = null;
        }

        public void LoggedWithScopes(string scope)
        {
            this.Principal = new ClaimsPrincipal(new ClaimsIdentity(new []
            {
                new Claim("scope", scope), 
            }));
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