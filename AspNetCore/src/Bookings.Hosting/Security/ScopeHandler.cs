using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Bookings.Hosting.Security
{
    public class ScopeHandler : AuthorizationHandler<ScopeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            ScopeRequirement requirement)
        {
            var scopeClaims = context.User.Claims.Where(a => a.Type == "scope");

            var scopes = scopeClaims.SelectMany(a => a.Value.Split(" ")).Distinct().ToArray();

            if (requirement.MatchAll(scopes))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}