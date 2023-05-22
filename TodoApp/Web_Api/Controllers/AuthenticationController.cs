using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Web_Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IConfiguration _config;

    public AuthenticationController(IConfiguration config)
    {
        _config = config;
    }

    public record Authentication(string? UserName, string? UserPassword);
    public record Users(int Id, string FirstName, string LastName, string UserName);

    // api/Authentication/tokenize
    [HttpPost("tokenize")]
    [AllowAnonymous]
    public ActionResult<string> Authenticate([FromBody] Authentication data)
    {
        var user = ValidateCredentials(data);

        if(user == null)
        {
            return Unauthorized();
        }

        var token = GenerateToken(user);

        return Ok(token);
    }

    private string GenerateToken(Users user)
    {
        var secretKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(_config.GetValue<string>("Authentication:SecretKey")!)    
        );

        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha512);

        List<Claim> claims = new()
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
        };

        var token = new JwtSecurityToken(
            _config.GetValue<string>("Authentication:Issuer"),
            _config.GetValue<string>("Authentication:Audience"),
            claims,
            DateTime.UtcNow,
            DateTime.UtcNow.AddMinutes(2),
            signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private Users? ValidateCredentials(Authentication data)
    {
        if(Compare(data.UserName, "koushik") && Compare(data.UserPassword, "1234"))
        {
            return new Users(2, "Koushik", "Saha", data.UserName!);
        }
        else if (Compare(data.UserName, "admin") && Compare(data.UserPassword, "1234"))
        {
            return new Users(1, "Admin", "User", data.UserName!);
        }

        return null;
    }

    private bool Compare(string? actual, string expected)
    {
        if(actual is not null && actual.Equals(expected))
        {
            return true;
        }

        return false;
    }
}
