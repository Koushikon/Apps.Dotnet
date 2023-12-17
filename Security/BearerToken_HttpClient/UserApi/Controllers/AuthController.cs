using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserApi.Database;
using UserApi.Models;

namespace UserApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
	private readonly UsersDatabase _database;

	public AuthController(UsersDatabase database)
	{
		_database = database;
	}

	[HttpPost("login")]
	public IActionResult Login([FromBody] AuthenticationModel login)
	{
		if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Password))
		{
			return BadRequest("Invalid client request.");
		}

		if (CheckRegisteredUser(login))
		{
			var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication"));
			var signingCeedentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
			var tokenOptions = new JwtSecurityToken(
				issuer: "https://localhost:7055",
				audience: "https://localhost:7055",
				claims: new List<Claim>(),
				expires: DateTime.Now.AddMinutes(10),
				signingCredentials: signingCeedentials
			);

			var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

			return Ok(new { token });
		}

		return Unauthorized();
	}


	private bool CheckRegisteredUser(AuthenticationModel user)
	{
		return _database.Exists(u =>
		u.Email.Equals(user.Email, StringComparison.InvariantCultureIgnoreCase) &&
		u.Password == user.Password);
	}
}