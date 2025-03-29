using System.Net;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.CustomMiddleware;

/***
 * It provides a pipeline through which requests flow, allowing us to intercept, process,
 * and modify requests and response objects. By implementing custom middleware,
 * we can incorporate API key authentication into our ASP.NET Core applications with flexibility and control.
 * 
 * Custom middleware allows us to intercept incoming requests and perform authentication
 * and authorization checks before the request reaches the endpoint. This is called before the attribute filters.
 */

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IApiKeyValidation _apiKeyValidation;

    public ApiKeyMiddleware(RequestDelegate next, IApiKeyValidation apiKeyValidation)
    {
        _next = next;
        _apiKeyValidation = apiKeyValidation;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string? apiKey = context.Request.Headers[Constants.ApiKeyHeaderName];

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return;
        }

        if (!_apiKeyValidation.IsValidKey(apiKey))
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return;
        }

        await _next(context);
    }
}