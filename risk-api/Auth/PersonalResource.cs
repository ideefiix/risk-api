using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace risk_api.Auth;

/**
 * CHECK THAT USER CLAIM IS EQUAL TO
 * THE OWNER OF THE REQUESTED RESOURCE
 */
public class PersonalResource : TypeFilterAttribute
{
    public PersonalResource() : base(typeof(PersonalResourceFilter))
    {
    }
}

public class PersonalResourceFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        try
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            context.HttpContext.Request.RouteValues.TryGetValue("Id", out var requestUserId);

            var clientToken = authorizationHeader.ToString().TrimStart("Bearer".ToCharArray());
            clientToken = clientToken.Trim();

            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(clientToken);
            var userId = jwt.Claims.FirstOrDefault(c => c.Type == "Id").Value;

            if (userId != requestUserId.ToString())
            {
                context.Result = new UnauthorizedResult();
            }
        }
        catch (Exception e)
        {
            //The route or token dont contain the ID 
            context.Result = new UnauthorizedResult();
            return;
        }
    }
}