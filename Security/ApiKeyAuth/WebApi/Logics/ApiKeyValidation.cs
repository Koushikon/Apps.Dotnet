using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Logics;

public class ApiKeyValidation : IApiKeyValidation
{
    private readonly IConfiguration _configuration;

    public ApiKeyValidation(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public bool IsValidKey(string userApiKey)
    {
        if (string.IsNullOrWhiteSpace(userApiKey))
        {
            return false;
        }

        string? apiKey = _configuration.GetValue<string>(Constants.ApiKeyName);

        if (apiKey == null || apiKey != userApiKey)
        {
            return false;
        }
        return true;
    }
}