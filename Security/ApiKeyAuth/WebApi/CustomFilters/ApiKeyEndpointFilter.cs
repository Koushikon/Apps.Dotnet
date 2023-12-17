using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.CustomFilters;

public class ApiKeyEndpointFilter : IEndpointFilter
{
    private readonly IApiKeyValidation _apiKeyValidation;

    public ApiKeyEndpointFilter(IApiKeyValidation apiKeyValidation)
    {
        _apiKeyValidation = apiKeyValidation;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        string? apiKey = context.HttpContext.Request.Headers[Constants.ApiKeyHeaderName];

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            return Results.BadRequest();
        }

        if (!_apiKeyValidation.IsValidKey(apiKey))
        {
            return Results.Unauthorized();
        }
        return await next(context);
    }
}