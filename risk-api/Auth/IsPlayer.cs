using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace risk_api.Auth;

public class IsPlayer : TypeFilterAttribute
{
    public IsPlayer() : base(typeof(IsPlayerFilter))
    {
    }

    public class IsPlayerFilter : IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var clientToken = authorizationHeader.ToString().TrimStart("Bearer".ToCharArray());
            clientToken = clientToken.Trim();

            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(clientToken);
            var claimInToken = jwt.Claims.FirstOrDefault(c => c.Type == "Id");

            if (claimInToken == null)
            {
                context.Result = new UnauthorizedResult();
            }
            

        }
    }
}