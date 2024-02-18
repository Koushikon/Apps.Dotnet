using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Web.Filters;

/***
 * Creating a class that can used to validate the Logic using Authorization Filter
 */

public class AuthorizeActionFilter : IAuthorizationFilter
{
    private readonly string _permission;

    public AuthorizeActionFilter(string permission)
    {
        _permission = permission;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        bool isAuthorized = CheckUserPermission(context.HttpContext.User, _permission);
        
        if(!isAuthorized)
        {
            context.Result = new UnauthorizedResult();
        }
    }

    private bool CheckUserPermission(ClaimsPrincipal user, string permission)
    {
        // Code for checking the user permission

        // Assuming the user has only Read permission
        return permission == "Read";
    }
}