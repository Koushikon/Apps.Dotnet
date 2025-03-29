namespace HttpContext_Items.Middleware;

public class CustomMiddleware
{
    private readonly RequestDelegate _next;
    public static readonly object MiddlewareObjectKey = new();


    public CustomMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        context.Items[MiddlewareObjectKey] = "Wheather forecast";
        await _next(context);
    }
}

public static class CustomMiddlewareExtension
{
    public static IApplicationBuilder UseCustomMiddlewareZ(this IApplicationBuilder app)
    {
        return app.UseMiddleware<CustomMiddleware>();
    }
}