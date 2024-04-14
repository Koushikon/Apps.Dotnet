using Microsoft.AspNetCore.Authorization;

namespace WebApp.Handlers;

public static class AuthorizationPolicyBuilderExtension
{
    public static AuthorizationPolicyBuilder UserRequireCustomClaim(this AuthorizationPolicyBuilder builder, string claimType)
    {
        builder.AddRequirements(new CustomUserRequireClaim(claimType));
        return builder;
    }
}