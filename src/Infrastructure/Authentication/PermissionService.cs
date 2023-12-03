using backend.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure.Authentication;

public class PermissionService: IPermissionService
{
    private readonly AppDbContext _context;

    public PermissionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<HashSet<string>> GetPermissionAsync(Guid userId)
    {
        var roles = await _context.Set<User>()
            .Include(u => u.Roles)
            .ThenInclude(r => r.Claims)
            .Where(u => u.Id == userId)
            .Select(u => u.Roles)
            .ToArrayAsync();

        return roles
            .SelectMany(x => x)
            .SelectMany(x => x.Claims)
            .Select(x => x.Name)
            .ToHashSet();
    }
}