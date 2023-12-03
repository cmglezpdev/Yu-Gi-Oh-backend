using backend.Common.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace backend.Infrastructure.Authentication;

public class PermissionAuthorizationHandler: AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
        PermissionRequirement requirement)
    {
        var permissions = context
            .User
            .Claims
            .Where(c => c.Type == CustomClaim.Permissions)
            .Select(c => c.Value)
            .ToHashSet();
        
        if (permissions.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}