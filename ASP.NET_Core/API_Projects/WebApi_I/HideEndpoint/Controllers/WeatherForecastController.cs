using Microsoft.AspNetCore.Mvc;

namespace HideEndpoint.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
	private static readonly string[] Summaries = new[]
	{
		"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
	};


	[HttpGet("GetWeatherForecast")]
	public IEnumerable<WeatherForecast> Get()
	{
		return GetWeatherForecastData();
	}

	// Using [NonAction] attribute to Hide the api endpoint from swagger
	[HttpGet("GetMethodOne")]
	[NonAction]
	public IEnumerable<WeatherForecast> GetMethodOne()
	{
		return GetWeatherForecastData();
	}


	// Using [ApiExplorerSettings(IgnoreApi = true)] attribute with parameter IgnoreApi is true
	// To hide the api endpoint from swagger
	[HttpGet("GetMethodTwo")]
	[ApiExplorerSettings(IgnoreApi = true)]
	public IEnumerable<WeatherForecast> GetMethodTwo()
	{
		return GetWeatherForecastData();
	}


	[HttpGet("GetMethodThree")]
	public IEnumerable<WeatherForecast> GetMethodThree()
	{
		return GetWeatherForecastData();
	}


	[HttpGet("GetMethodFour")]
	public IEnumerable<WeatherForecast> GetMethodFour()
	{
		return GetWeatherForecastData();
	}


	private IEnumerable<WeatherForecast> GetWeatherForecastData()
	{
		return Enumerable.Range(1, 5).Select(index => new WeatherForecast
		{
			Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
			TemperatureC = Random.Shared.Next(-20, 55),
			Summary = Summaries[Random.Shared.Next(Summaries.Length)]
		})
		.ToArray();
	}
}