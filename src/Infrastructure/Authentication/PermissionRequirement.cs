using Microsoft.AspNetCore.Authorization;

namespace backend.Infrastructure.Authentication;

public class PermissionRequirement: IAuthorizationRequirement
{
    public string Permission { get; }
    public PermissionRequirement(string permission)
    {
        Permission = permission;
    }

}