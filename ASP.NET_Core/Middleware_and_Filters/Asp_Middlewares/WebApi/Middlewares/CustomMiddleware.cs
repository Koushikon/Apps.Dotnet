namespace WebApi.Middlewares;


/***
 * Creating a Custom Middleware class which will be used in the Application's Pipeline.
 * 
 * Here, we are injecting the RequestDelegate next parameter and use it inside the InvokeAsync
 * method to pass the execution to the next component. It is important that our middleware contains
 * a public constructor with the injected RequestDelegate parameter and the method named Invoke or InvokeAsync.
 */


public class CustomMiddleware
{
    private readonly RequestDelegate _next;

    public CustomMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Console.WriteLine("Custom middleware logic from the CustomMiddleware started.");
        await _next.Invoke(context);
        Console.WriteLine("Custom middleware logic from the CustomMiddleware ended.");
    }
}