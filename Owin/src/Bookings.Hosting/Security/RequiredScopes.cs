using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Bookings.Hosting.Security
{
    public class RequiredScopes : AuthorizeAttribute
    {
        private string[] required;

        public RequiredScopes(params string[] scopes)
        {
            this.required = scopes;
            this.Roles = string.Join(" ", scopes);
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            if (actionContext.RequestContext.Principal is ClaimsPrincipal principal)
            {
                var scopes = principal.Claims.Where(a => a.Type == "scope").SelectMany(a => a.Value.Split(' '))
                    .Distinct();
                if (required.All(scopes.Contains)) return true;
            }
            return base.IsAuthorized(actionContext);
        }
    }
}