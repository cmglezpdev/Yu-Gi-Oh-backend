using backend.Common.Enums;
using backend.Infrastructure;
using backend.Infrastructure.Common;
using backend.Infrastructure.Entities;
using backend.Presentation.DTOs.Role;
using Microsoft.EntityFrameworkCore;

namespace backend.Application.Services;

public class RoleService
{
    private readonly AppDbContext _context;

    public RoleService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<McResult<List<RoleEntity>>> GetAllRolesAsync()
    {
        var roles = await _context.Roles
            .Include(r => r.Claims)
            .ToListAsync();
        return McResult<List<RoleEntity>>.Succeed(roles);
    }
    
    public async Task<McResult<RoleEntity>> GetRoleByIdAsync(Guid id)
    {
        var role = await _context.Roles
            .Include(r => r.Claims)
            .Where(r => r.Id == id)
            .FirstOrDefaultAsync();
        
        return role is null 
            ? McResult<RoleEntity>.Failure("Role not found") 
            : McResult<RoleEntity>.Succeed(role);
    }

    public async Task<McResult<string>> UpdateRolePermissionsAsync(Guid roleId, UpdateRolePermissionDto dto)
    {
        var roleResponse = await GetRoleByIdAsync(roleId);
        if (!roleResponse.IsSuccess)
            return McResult<string>.Failure(roleResponse.ErrorMessage, ErrorCodes.InvalidInput);

        var claim = await _context.Claims
            .Where(c => c.Id == dto.ClaimId)
            .FirstOrDefaultAsync();
        
        if (claim is null)
            return McResult<string>.Failure("Claim not found", ErrorCodes.InvalidInput);
        
        var role = roleResponse.Result;
        var roleHasClaim = role.Claims.Any(c => c.Id == claim.Id);

        if (dto.ToAdd)
        {
            if(roleHasClaim) McResult<string>.Failure("The role already has this claim", ErrorCodes.InvalidInput);
            role.Claims.Add(claim);
        }
        else
        {
            if(!roleHasClaim) McResult<string>.Failure("The role does not have this claim", ErrorCodes.InvalidInput);
            role.Claims.Remove(claim);
        }
        
        await _context.SaveChangesAsync();
        return McResult<string>.Succeed("Role permissions updated");
    }
}