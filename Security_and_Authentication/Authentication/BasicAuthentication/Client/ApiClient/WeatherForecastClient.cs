using System.Net.Http.Headers;
using System.Text;

namespace Client.ApiClient;

public class WeatherForecastClient
{
    private readonly HttpClient _httpClient;

    public WeatherForecastClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://localhost:7258");

        var username = configuration.GetValue<string>("ApiSecret:BasicAuthenticationUsername");
        var password = configuration.GetValue<string>("ApiSecret:BasicAuthenticationPassword");
        var basicAuthenticatedValue = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", basicAuthenticatedValue);
    }

    public async Task<string> GetAsync() => await _httpClient.GetStringAsync("WeatherForecast");
}