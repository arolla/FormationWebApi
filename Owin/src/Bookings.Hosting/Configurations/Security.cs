using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading;
using System.Web.Http;
using Bookings.Hosting.Security;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Swashbuckle.Application;

namespace Bookings.Hosting.Configurations
{
    public static class Security
    {
        public static IAppBuilder ConfigureSecurity(this IAppBuilder appBuilder, HttpConfiguration configuration)
        {
            configuration.Filters.Add(new RequiredScopes(Scopes.ApiV1));
            const string openIdDiscoveryEndpoint = @"http://localhost:8080/auth/realms/master/.well-known/openid-configuration";

            

            var handler = new JwtSecurityTokenHandler();
            
            var options = new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = @"http://localhost:8080/auth/realms/master",
                    ValidateAudience = false,
                    IssuerSigningKeyResolver = (token, securityToken, kid, parameters) =>
                    {
                        var openidConfigManager = new ConfigurationManager<OpenIdConnectConfiguration>(openIdDiscoveryEndpoint, new OpenIdConnectConfigurationRetriever(), new HttpDocumentRetriever{RequireHttps = false});
                        var openidConfig = openidConfigManager.GetConfigurationAsync().GetAwaiter().GetResult();
                        return openidConfig.JsonWebKeySet.GetSigningKeys();
                    },
                },
                Provider = new OAuthBearerAuthenticationProvider(),
                Realm = "master",
                TokenHandler = handler
            };
            appBuilder.UseJwtBearerAuthentication(options);
            return appBuilder;
        }
        
        public static SwaggerDocsConfig ConfigureSecurity(this SwaggerDocsConfig config)
        {
            config.OAuth2("oauth2")
                .Scopes(scopes => scopes.Add(Scopes.ApiV1, "Global scope for V1"))
                .AuthorizationUrl("http://localhost:8080/auth/realms/master/protocol/openid-connect/auth")
                .Flow("implicit");
            config.OperationFilter<AssignOAuth2SecurityRequirements>();
            return config;
        }

        public static SwaggerUiConfig ConfigureSecurity(this SwaggerUiConfig config)
        {
            config.EnableOAuth2Support("demo", "master", "test");
            return config;
        }
    }
}