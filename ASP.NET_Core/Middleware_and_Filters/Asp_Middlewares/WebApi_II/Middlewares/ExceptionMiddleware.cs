namespace WebApi_II.Middlewares;

// We may need the Microsoft.AspNetCore.Http.Abstractions package into our project
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        Console.WriteLine("Coming from Dynamic middleware.");
        await httpContext.Response.WriteAsync($"Coming from Dynamic ExceptionMiddleware middleware.");
        await _next.Invoke(httpContext);
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionMiddleware>();
    }
}