using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Bookings.Hosting.Security
{
    public class ScopeRequirement : IAuthorizationRequirement
    {
        private readonly string[] requiredScopes;

        public ScopeRequirement(params string[] requiredScopes)
        {
            this.requiredScopes = requiredScopes;
        }

        public bool MatchAll(string[] scopes)
        {
            return this.requiredScopes.All(scopes.Contains);
        }
    }

    public static class AuthorizationPolicyBuilderExtensions
    {
        public static AuthorizationPolicyBuilder RequireScopes(this AuthorizationPolicyBuilder builder, params string[] scopes)
        {
            builder.AddRequirements(new ScopeRequirement(scopes));
            return builder;
        }
    }
}