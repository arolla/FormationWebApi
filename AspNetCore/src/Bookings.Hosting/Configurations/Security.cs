using System.Collections.Generic;
using Bookings.Hosting.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Bookings.Hosting.Configurations
{
    public static class Security
    {
        public static IApplicationBuilder ConfigureSecurity(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseAuthentication();
            return applicationBuilder;
        }

        public static IServiceCollection AddSecurity(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configure =>
            {
                configure.RequireHttpsMetadata = false;
                configure.Authority =
                    @"http://localhost:8080/auth/realms/master";
                configure.TokenValidationParameters.ValidateAudience = false;
            });
            services.AddSingleton<IAuthorizationHandler, ScopeHandler>();
            services.AddAuthorization(a =>
                
                a.AddPolicy(Policies.GlobalScope,
                    policy => policy.RequireScopes(Scopes.ApiV1)));
            return services;
        }



        public static SwaggerGenOptions ConfigureSecurity(this SwaggerGenOptions options)
        {
            options.AddSecurityDefinition("oauth2", new OAuth2Scheme
            {
                Type = "oauth2",
                Flow = "implicit",
                AuthorizationUrl = "http://localhost:8080/auth/realms/master/protocol/openid-connect/auth",
                Scopes = new Dictionary<string, string>
                {
                    {Scopes.ApiV1, "Global scope for V1"}
                }
            });
            options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
            {
                {"oauth2", new[] {Scopes.ApiV1}}
            });
            return options;
        }

        public static SwaggerUIOptions ConfigureSecurity(this SwaggerUIOptions options)
        {
            options.OAuthClientId("demo");
            options.OAuthAppName("test");
            options.OAuthRealm("master");
            return options;
        }
    }
}