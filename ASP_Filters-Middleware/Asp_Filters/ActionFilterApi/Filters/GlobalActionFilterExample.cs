using Microsoft.AspNetCore.Mvc.Filters;

namespace ActionFilterApi.Filters;

public class GlobalActionFilterExample : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine("Filter:\tCalling Global OnActionExecuting.");
        // This code executes before Action method code executes
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine("Filter:\tCalling Global OnActionExecuted.\n");
        // This code executes after Action method code executes
    }
}