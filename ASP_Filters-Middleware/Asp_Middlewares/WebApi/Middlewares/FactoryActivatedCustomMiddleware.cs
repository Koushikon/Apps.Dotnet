
namespace WebApi.Middlewares;

public class FactoryActivatedCustomMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        Console.WriteLine("Factory activated custom middleware logic from the separate class started.");
        await next.Invoke(context);
        Console.WriteLine("Factory activated custom middleware logic from the separate class ended.");
    }
}