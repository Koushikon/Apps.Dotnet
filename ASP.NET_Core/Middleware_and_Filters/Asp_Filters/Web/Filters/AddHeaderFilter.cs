using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Filters;

public class AddHeaderFilter : IResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
        context.HttpContext.Response.Headers.Append("OnResultExecuting", "This header is added with IResultFilter.");
    }

    public void OnResultExecuted(ResultExecutedContext context)
    { }
}