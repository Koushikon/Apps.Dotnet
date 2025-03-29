using Microsoft.AspNetCore.Authorization;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.PolicyBased;

public class ApiKeyHandler : AuthorizationHandler<ApiKeyRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IApiKeyValidation _apiKeyValidation;

    public ApiKeyHandler(IHttpContextAccessor httpContextAccessor, IApiKeyValidation apiKeyValidation)
    {
        _httpContextAccessor = httpContextAccessor;
        _apiKeyValidation = apiKeyValidation;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ApiKeyRequirement requirement)
    {
        string? apiKey = _httpContextAccessor?.HttpContext?.Request.Headers[Constants.ApiKeyHeaderName];

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            context.Fail();
            return Task.CompletedTask;
        }

        if (!_apiKeyValidation.IsValidKey(apiKey))
        {
            context.Fail();
            return Task.CompletedTask;
        }

        context.Succeed(requirement);
        return Task.CompletedTask;
    }
}