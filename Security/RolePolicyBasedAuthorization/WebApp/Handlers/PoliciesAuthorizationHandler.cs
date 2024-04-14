using Microsoft.AspNetCore.Authorization;

namespace WebApp.Handlers;

public class PoliciesAuthorizationHandler : AuthorizationHandler<CustomUserRequireClaim>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomUserRequireClaim requirement)
    {
        if (context.User == null || context.User.Identity == null || !context.User.Identity.IsAuthenticated)
        {
            context.Fail();
            return Task.CompletedTask;
        }

        // Checks UserPolicy Policy with "Email" and "DateOfBirth" Claim one at a time
        bool hasClaim = context.User.Claims.Any(x => x.Type == requirement.ClaimType);

        if (hasClaim)
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        context.Fail();
        return Task.CompletedTask;
    }
}

public class CustomUserRequireClaim : IAuthorizationRequirement
{
    public string ClaimType { get; }

    public CustomUserRequireClaim(string claimType)
    {
        ClaimType = claimType;
    }
}