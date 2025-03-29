using ActionFilterApi.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ActionFilterApi.Filters;

public class ValidateModelFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine("Validate Filter:\tCalling OnActionExecuting.");

        var aParam = context.ActionArguments.SingleOrDefault(p => p.Value is IEntity);

        if (aParam.Value == null)
        {
            context.Result = new BadRequestObjectResult("Model is null.");
            return;
        }

        if (!context.ModelState.IsValid)
        {
            context.Result = new UnprocessableEntityObjectResult(context.ModelState);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine("Validate Filter:\tCalling OnActionExecuted.");
    }
}