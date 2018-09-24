using System.Security.Claims;
using Bookings.Hosting.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Bookings.Hosting.Tests
{
    public class TestsStartup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc(o => o.Filters.Add(new AuthorizeFilter(Policies.GlobalScope)))
                    .AddApplicationPart(typeof(Startup).Assembly)
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSecurity();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.Use(async (context, next) =>
            {
                context.User = app.ApplicationServices.GetService<TestUserContext>().Principal;
                await next();
            });
            app.ConfigureSecurity();
            app.UseMvc();
        }
    }

    public class TestUserContext
    {
        internal ClaimsPrincipal Principal { get; private set; }

        public void WithGlobalScope()
        {
            this.Principal = new ClaimsPrincipal(new ClaimsIdentity(new[]
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
            }, JwtBearerDefaults.AuthenticationScheme));
        }
    }
}