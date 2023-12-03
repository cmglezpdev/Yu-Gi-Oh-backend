using backend.Application.Services;
using backend.Common.Authorization;
using backend.Infrastructure;
using backend.Infrastructure.Authentication;
using backend.Presentation.DTOs.Role;
using Microsoft.AspNetCore.Mvc;

namespace backend.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoleController: ControllerBase
{
    private readonly RoleService _roleService;

    public RoleController(RoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    [HasPermission(Permission.ReadRole)]
    public async Task<IActionResult> GetAllRolesAsync()
    {
        var roles = await _roleService.GetAllRolesAsync();
        return Ok(roles);
    }
    
    [HttpGet("{id:Guid}")]
    [HasPermission(Permission.ReadRole)]
    public async Task<IActionResult> GetRoleByIdAsync(Guid id)
    {
        var role = await _roleService.GetRoleByIdAsync(id);
        return role.IsSuccess 
            ? Ok(role) 
            : NotFound(role);
    }
    
    [HttpPatch("{id:Guid}/update-permissions")]
    [HasPermission(Permission.WriteRole)]
    public async Task<IActionResult> UpdateRolePermissionsAsync(Guid id, [FromBody] UpdateRolePermissionDto dto)
    {
        var result = await _roleService.UpdateRolePermissionsAsync(id, dto);
        return result.IsSuccess 
            ? Ok(result) 
            : BadRequest(result);
    }
}