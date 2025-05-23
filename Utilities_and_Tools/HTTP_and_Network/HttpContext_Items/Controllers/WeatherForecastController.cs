using HttpContext_Items.Filter;
using HttpContext_Items.Middleware;
using Microsoft.AspNetCore.Mvc;

namespace HttpContext_Items.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    /***
     * [ServiceFilter(typeof(CustomFilter))]
     * CustomFilter implements IActionFilter
     * Which is using HttpContext Items and Logging information
     */
    [HttpGet(Name = "GetWeatherForecast")]
    [ServiceFilter(typeof(CustomFilter))]
    public IEnumerable<WeatherForecast> Get()
    {
        HttpContext.Items.TryGetValue(CustomMiddleware.MiddlewareObjectKey, out var middlewareValue);
        _logger.LogInformation("Middleware value: {MV}", middlewareValue ?? "Middleware value not set.");

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }



}