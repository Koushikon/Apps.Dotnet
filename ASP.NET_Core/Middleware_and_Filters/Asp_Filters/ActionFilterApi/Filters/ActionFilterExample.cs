using Microsoft.AspNetCore.Mvc.Filters;

namespace ActionFilterApi.Filters;

public class ActionFilterExample : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine("Filter:\tCalling Action OnActionExecuting.");
        // This code executes before Action method code executes
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine("Filter:\tCalling Action OnActionExecuted.");
        // This code executes after Action method code executes
    }
}