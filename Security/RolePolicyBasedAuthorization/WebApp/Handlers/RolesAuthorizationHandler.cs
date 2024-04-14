using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using WebApp.Models;

namespace WebApp.Handlers;

public class RolesAuthorizationHandler : AuthorizationHandler<RolesAuthorizationRequirement>, IAuthorizationHandler
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RolesAuthorizationRequirement requirement)
    {
        if(context.User == null || context.User.Identity == null || !context.User.Identity.IsAuthenticated)
        {
            context.Fail();
            return Task.CompletedTask;
        }

        bool validRole = false;

        // If None of the Roles are set
        if (requirement.AllowedRoles == null
            || requirement.AllowedRoles.Any() == false)
        {
            validRole = true;
        }
        else
        {
            // Checks the logged in user's claim "UserName" with the provided Roles value "Admin" or "User"
            var claims = context.User.Claims;
            var userClaim = claims.FirstOrDefault(x => x.Type == "UserName");
            string? userName = userClaim?.Value;
            var roles = requirement.AllowedRoles;

            validRole = new Users().GetUsers().Where(x => roles.Contains(x.Role) && x.UserName == userName).Any();
        }

        if (validRole)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
        return Task.CompletedTask;
    }
}