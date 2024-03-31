using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
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
    public ActionResult Login(LoginRequest req)
    {
        Player? player = _context.Players.FirstOrDefault(p => p.Username == req.Username);
        //Leaks too much information to client. But this is not a bank.
        if (player == null) return BadRequest("User not found");

        var hasher = new PasswordHasher<Player>();
        var validationResult = hasher.VerifyHashedPassword(player, player.Password, req.Password);
        
        if (validationResult == PasswordVerificationResult.Failed) return BadRequest("Invalid password");

        var token = CreateToken(player);

        return Ok(token);
    }

    private string CreateToken(Player player)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim("Username", player.Username),
            new Claim("Id", player.Id.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));
        var issuer = _config.GetSection("Jwt:Issuer").Value;

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            
        var token = new JwtSecurityToken(
            audience: issuer,
            issuer: issuer,
            claims: claims,
            expires: DateTime.Now.AddDays(7),
            signingCredentials: credentials
        );
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
}