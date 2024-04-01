using System.Data;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;

namespace risk_api.Auth;

public static class AuthValidator
{
    public static bool UserHasId(HttpRequest request, string id)
    {
        try
        {
            var token = ReadToken(request);
            if (token == null) throw new EvaluateException("Could not read token");
            var userId = token.Claims.FirstOrDefault(c => c.Type == "Id").Value;

            if (userId == id) return true;

            return false;
        }
        catch (Exception e)
        {
            //The route or token dont contain the ID 
            return false;
        }
    }

    private static JwtSecurityToken? ReadToken(HttpRequest request)
    {
        if (!request.Headers.TryGetValue("Authorization", out var authorizationHeader))
        {
            return null;
        }

        var clientToken = authorizationHeader.ToString().TrimStart("Bearer".ToCharArray());
        clientToken = clientToken.Trim();

        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(clientToken);
        return jwt;
    }
}