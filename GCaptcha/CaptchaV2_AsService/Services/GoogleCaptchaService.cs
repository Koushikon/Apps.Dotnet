using CaptchaV2_AsService.Models;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text.Json;

namespace CaptchaV2_AsService.Services;

public class GoogleCaptchaService
{
    private readonly IOptionsMonitor<GoogleCaptchaConfig> _config;

    public GoogleCaptchaService(IOptionsMonitor<GoogleCaptchaConfig> config)
    {
        _config = config;
    }

    public async Task<bool> VerifyTokenStatus(string token)
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

        return response.Success;
    }

    public async Task<GoogleCaptchaResponse?> VerifyTokenResult(string token)
    {
        string url = $"https://www.google.com/recaptcha/api/siteverify?secret={_config.CurrentValue.SecretKey}&response={token}";

        using var client = new HttpClient();
        var httpResult = await client.GetAsync(url);

        if (httpResult.StatusCode != HttpStatusCode.OK)
        {
            return null;
        }

        var responceString = await httpResult.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var response = JsonSerializer.Deserialize<GoogleCaptchaResponse>(responceString, options)!;

        return response;
    }
}