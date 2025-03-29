using ActionFilterApi.Contracts;
using ActionFilterApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ActionFilterApi.Filters;

public class ValidateEntityExistsFilter<T> : IActionFilter where T: class, IEntity
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine("Validate Entity Exists Filter:\t In Action Calling OnActionExecuting.");

        Guid id = Guid.Empty;

        if (context.ActionArguments.ContainsKey("id"))
        {
            id = (Guid)context.ActionArguments["id"]!;
        }
        else
        {
            context.Result = new BadRequestObjectResult("Bad id parameter");
            return;
        }


        /***
         * If Data is not found in the database with this id return NotFound,
         * Otherwise store the data with entity key name inside HttpContext item
         */
        //Movie movie = null;
        var movie = new Movie();
        if (movie == null)
        {
            context.Result = new NotFoundResult();
        }
        else
        {
            movie.Id = new Guid();
            movie.Name = "Toy Story";
            movie.Genre = "Cartoon";
            context.HttpContext.Items.Add("entity", movie);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine("Validate Entity Exists Filter:\t In Action Calling OnActionExecuted.");
    }    
}