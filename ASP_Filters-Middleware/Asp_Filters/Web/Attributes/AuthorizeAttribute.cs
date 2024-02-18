using Microsoft.AspNetCore.Mvc;
using Web.Filters;

namespace Web.Attributes;

/***
 * Creating a class which is used as TypeFilterAttribute and pass the input parameter that is used for AuthorizeActionFilter inside a Controller or Action
 */

public class AuthorizeAttribute : TypeFilterAttribute
{
    public AuthorizeAttribute(string permission)
        : base(typeof(AuthorizeActionFilter))
    {
        Arguments = new[] { permission };
    }
}