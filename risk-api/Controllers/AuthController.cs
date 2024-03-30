using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using risk_api.Controllers.Requests;
using risk_api.DAL.DBContext;
using risk_api.Models;

namespace risk_api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly DatabaseContext _context;
    private readonly IConfiguration _config;

    public AuthController(DatabaseContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }
    
    [HttpPost("login")]
    public Task<ActionResult> Login(LoginRequest req)
    {
        Player? player = _context.Players.FirstOrDefault(p => p.Username == req.Username);

        if (player == null) return BadRequest("User not found");

        if (!BCrypt.Net.BCrypt.Verify(req.Password, player.PasswordHash)) return BadRequest("Invalid password");

        var token = CreateToken(player);

        return Ok(token);
    }

    private string CreateToken(Player player)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim("playerName", player.PlayerName),
            new Claim("id", player.PlayerId.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Token").Value!));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
        );
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
}