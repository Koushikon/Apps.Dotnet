using WebApi.Middlewares;

namespace WebApi.Extensions;

/***
 * We're creating a class and place a single extension method inside:
 * 
 * Inside that creating a static method that returns an IApplicationBuilder as a result
 * and extends the IApplicationBuilder interface. In the method’s body,
 * we just call the UseMiddleware method to add our custom middleware to the application’s pipeline.
 * 
 * This way we can add multiple Middleware to the application’s pipeline.
 */

public static class CustomMiddlewareExtension
{
    // Custom Middleware in Separate Class
    public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder builder) =>
        builder.UseMiddleware<CustomMiddleware>();

    // Factory Based Custom Middleware
    public static IApplicationBuilder UseFactoryActivatedCustomMiddleware(this IApplicationBuilder builder) =>
        builder.UseMiddleware<FactoryActivatedCustomMiddleware>();
}