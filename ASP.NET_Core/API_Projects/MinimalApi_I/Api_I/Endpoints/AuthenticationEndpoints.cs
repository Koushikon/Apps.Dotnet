using Api_I.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api_I.Endpoints;

public static class AuthenticationEndpoints
{
    public static void AddAuthenticationEndpoint(this WebApplication app)
    {
        app.MapPost("/api/tokenize", (IConfiguration config, [FromBody] Authentication data) =>
        {
            var user = ValidateCredentials(data);

            if (user == null)
            {
                return Results.Unauthorized();
            }

            var token = GenerateToken(user, config);

            return Results.Ok(token);
        }).AllowAnonymous();
    }

    private static string GenerateToken(Users user, IConfiguration config)
    {
        var secretKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(config.GetValue<string>("Authentication:SecretKey")!)
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
            config.GetValue<string>("Authentication:Issuer"),
            config.GetValue<string>("Authentication:Audience"),
            claims,
            DateTime.UtcNow,
            DateTime.UtcNow.AddMinutes(2),
            signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static Users? ValidateCredentials(Authentication data)
    {
        if (Compare(data.UserName, "koushik") && Compare(data.UserPassword, "1234"))
        {
            return new Users(2, "Koushik", "Saha", data.UserName!);
        }
        else if (Compare(data.UserName, "admin") && Compare(data.UserPassword, "1234"))
        {
            return new Users(1, "Admin", "User", data.UserName!);
        }

        return null;
    }

    private static bool Compare(string? actual, string expected)
    {
        if (actual is not null && actual.Equals(expected))
        {
            return true;
        }

        return false;
    }
}