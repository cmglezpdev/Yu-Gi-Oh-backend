namespace backend.Infrastructure.Authentication;

public interface IPermissionService
{
    Task<HashSet<string>> GetPermissionAsync(Guid userId);
}