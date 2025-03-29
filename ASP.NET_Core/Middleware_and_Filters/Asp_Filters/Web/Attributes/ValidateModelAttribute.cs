using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Attributes;

/***
 * Creating a class which inherits ActionFilterAttribute and implements Before Action Filter
 * To validate the model if its null or if the model is valid.
 */

public class ValidateModelAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var param = context.ActionArguments.SingleOrDefault();
        //base.OnActionExecuting(context);

        if(param.Value == null)
        {
            context.Result = new BadRequestObjectResult("Model is null.");
            return;
        }

        if (!context.ModelState.IsValid)
        {
            context.Result = new UnprocessableEntityObjectResult(context.ModelState);
        }
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        //base.OnActionExecuted(context);
    }
}