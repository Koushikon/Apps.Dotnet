using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.CustomFilters;

public class ApiKeyAuthFilter : IAuthorizationFilter
{
    private readonly IApiKeyValidation _apiKeyValidation;

    public ApiKeyAuthFilter(IApiKeyValidation apiKeyValidation)
    {
        _apiKeyValidation = apiKeyValidation;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        string? apiKey = context.HttpContext.Request.Headers[Constants.ApiKeyHeaderName];

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            context.Result = new BadRequestResult();
            return;
        }

        if (!_apiKeyValidation.IsValidKey(apiKey))
        {
            context.Result = new UnauthorizedResult();
        }
    }
}