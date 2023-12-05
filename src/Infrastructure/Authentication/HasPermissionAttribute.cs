using Microsoft.AspNetCore.Authorization;

namespace backend.Infrastructure.Authentication;

public sealed class HasPermissionAttribute: AuthorizeAttribute
{   
    public HasPermissionAttribute(string permission): base(policy: permission)
    {
    }
}