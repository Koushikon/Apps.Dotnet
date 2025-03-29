using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;

namespace Server.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };


    // GET: WeatherForecast/GetWeatherForecast
    [HttpGet(Name = "GetWeatherForecast")]
    public IActionResult Get()
    {
        // Checking wheather user is authenticated or not
        if (!IsRequestAuthenticated())
        {
            return Unauthorized();
        }

        return Ok(Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        }));
    }

    private bool IsRequestAuthenticated()
    {
        try
        {
            // When Authorization header cannot be parsed return false
            if (!AuthenticationHeaderValue.TryParse(Request.Headers.Authorization, out var basicAuthCredential))
            {
                return false;
            }

            // When Credential Scheme is not Basic and Credential parameter is empty return false
            if (basicAuthCredential.Scheme != "Basic" || string.IsNullOrWhiteSpace(basicAuthCredential.Parameter))
            {
                return false;
            }

            var usernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(basicAuthCredential.Parameter));

            // When decoded parameter is empty return false
            if (string.IsNullOrWhiteSpace(usernamePassword))
            {
                return false;
            }

            var credentials = usernamePassword.Split(":");

            if (credentials.Length == 2 && credentials[0] == "Admin" && credentials[1] == "1234")
            {
                return true;
            }

            // if nothing else return false
            return false;
        }
        catch(Exception ex)
        {
            throw new Exception("Gets an error. ", ex);
        }
    }
}