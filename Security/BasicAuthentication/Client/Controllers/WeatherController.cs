using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;

namespace Client.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherController : ControllerBase
{
    private static readonly HttpClient _httpClient = new();
    private readonly IConfiguration _configuration;

    public WeatherController(IConfiguration configuration)
    {
        _httpClient.BaseAddress = new Uri("https://localhost:7258");
        _configuration = configuration;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IActionResult> Get()
    {
        /***
         * Getting the authentication username and password from User Secrets
         * Then building the base64 value out of it.
         */
        var username = _configuration.GetValue<string>("ApiSecret:BasicAuthenticationUsername");
        var password = _configuration.GetValue<string>("ApiSecret:BasicAuthenticationPassword");
        var basicAuthenticatedValue = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));

        /***
         * Clear up the web request header then set the Authorization header.
         * And, If we didn't provide any header we'll get 500 error
         * 
         */
        //_httpClient.DefaultRequestHeaders.Clear();
        //_httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {basicAuthenticatedValue}");

        /***
         * We can simplify the header adding process a bit more
         * By adding Authorization request header and passing the value.
         */
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", basicAuthenticatedValue);

        var response = await _httpClient.GetStringAsync("WeatherForecast");

        return Content(response, "application/json");
    }
}