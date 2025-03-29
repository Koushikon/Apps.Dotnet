using Microsoft.AspNetCore.Mvc.Filters;

namespace ActionFilterApi.Filters;

public class AsyncActionFilterExample : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        Console.WriteLine("Filter:\tCalling Before Async Action OnActionExecutionAsync.");

        // This code executes before Action method code executes
        var result = await next();
        // This code executes after Action method code executes

        Console.WriteLine("Filter:\tCalling After Async Action OnActionExecutionAsync.");
    }
}