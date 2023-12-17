using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;

namespace APISecurity.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IConfiguration _config;
    public record Authentication(string? UserName, string? Password);
    public record Users(int UserId, string EmpId, string UserName, string UserRole);

    public AuthenticationController(IConfiguration config)
    {
        _config = config;
    }

    // POST: api/Authentication/tokenize
    [HttpPost("tokenize")]
    // Even if we have Authorization at Controller or Settings level for entire application and
    // With [AllowAnonymous] attribure we can allow user to access route without authtication,
    // Opposite to [Authorize] attribute
    [AllowAnonymous]
    public ActionResult<string> Authenticate([FromBody] Authentication data)
    {
        var usersData = ValidateCredentials(data);

        if(usersData is null)
            return Unauthorized();

        var token = GenerateToken(usersData);

        return Ok(token);
    }

    #region Methods
    private string GenerateToken(Users users)
    {
        var secretKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(_config.GetValue<string>("Authentication:SecretKey")!)
        );

        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha512);

        List<Claim> claims = new()
        {
            new Claim(JwtRegisteredClaimNames.Sub, users.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, users.UserName.ToString()),
            new Claim("employeeId", users.EmpId),
            new Claim("title", users.UserRole),
        };

        var token = new JwtSecurityToken(
            _config.GetValue<string>("Authentication:Issuer"),
            _config.GetValue<string>("Authentication:Audience"),
            claims,
            DateTime.UtcNow,                // When will token become valid
            DateTime.UtcNow.AddMinutes(1),  // When will token expire
            signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private Users? ValidateCredentials(Authentication data)
    {
        if(Compare(data.UserName, "koushik") && Compare(data.Password, "koushik1234"))
        {
            return new Users(1, "Emp001",data.UserName!, "Owner");
        }
        else if (Compare(data.UserName, "nicky") && Compare(data.Password, "nicky1234"))
        {
            return new Users(2, "Emp002", data.UserName!, "Reader");
        }
        else if (Compare(data.UserName, "luffy") && Compare(data.Password, "luffy1234"))
        {
            return new Users(5, "Emp007", data.UserName!, "Admin");
        }
        return null;
    }

    private bool Compare(string? actual, string expected)
    {
        if(actual is not null && actual.Equals(expected, StringComparison.InvariantCultureIgnoreCase))
        {
            return true;
        }
        return false;
    }
    #endregion
}
