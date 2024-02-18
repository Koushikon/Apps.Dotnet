using Microsoft.AspNetCore.Mvc.Filters;

namespace ActionFilterApi.Filters;

public class ControllerActionFilterExample : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine("Filter:\tCalling Controller OnActionExecuting.");
        // This code executes before Action method code executes
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine("Filter:\tCalling Controller OnActionExecuted.");
        // This code executes after Action method code executes
    }
}