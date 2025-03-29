using CaptchaV3.Models;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text.Json;

namespace CaptchaV3.Services;

public class GoogleCaptchaService
{
    private readonly IOptionsMonitor<GoogleCaptchaConfig> _config;

    public GoogleCaptchaService(IOptionsMonitor<GoogleCaptchaConfig> config)
    {
        _config = config;
    }

    public async Task<bool> VerifyTokenStatus(string token)
    {
        try
        {
            string url = $"https://www.google.com/recaptcha/api/siteverify?secret={_config.CurrentValue.SecretKey}&response={token}";

            using var client = new HttpClient();
            var httpResult = await client.GetAsync(url);

            if (httpResult.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }

            var responceString = await httpResult.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var response = JsonSerializer.Deserialize<GoogleCaptchaResponse>(responceString, options)!;

            return response.Success && response.Score >= 0.5;
        }
        catch (Exception)
        {
            return false;
        }
    }
}