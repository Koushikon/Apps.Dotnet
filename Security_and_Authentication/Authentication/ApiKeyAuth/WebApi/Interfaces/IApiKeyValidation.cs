namespace WebApi.Interfaces;

public interface IApiKeyValidation
{
    bool IsValidKey(string userApiKey);
}