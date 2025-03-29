using Client.ApiClient;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ForecastController : ControllerBase
{
    private readonly WeatherForecastClient _weatherClient;

    public ForecastController(WeatherForecastClient weatherClient)
    {
        _weatherClient = weatherClient;
    }

    /***
     * More concise way to use an api to create a service for that
     * And whenever needed use that service.
     */
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var response = await _weatherClient.GetAsync();

        return Content(response, "application/json");
    }
}