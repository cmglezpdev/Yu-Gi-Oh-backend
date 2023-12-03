using AutoMapper;
using backend.Application.Repositories;
using backend.Common.Enums;
using backend.Infrastructure;
using backend.Infrastructure.Common;
using backend.Infrastructure.Entities;
using backend.Presentation.DTOs.User;
using Microsoft.EntityFrameworkCore;

namespace backend.Application.Services;
public class UserService
{
    private readonly AppDbContext _context;
    private readonly IDeckRepository _deckRepository;
    private readonly RoleService _roleService;
    private readonly IUserRepository _userRepository;

    public UserService(AppDbContext context, IDeckRepository deckRepository, RoleService roleService, IUserRepository userRepository)
    {
        _context = context;
        _deckRepository = deckRepository;
        _roleService = roleService;
        _userRepository = userRepository;
    }

    public async Task<McResult<IEnumerable<Deck>>> GetDecksByUserAsync(Guid id)
    {
        return await _userRepository.GetDecksByUserAsync(id);
    }

    public async Task<McResult<IEnumerable<Tournament>>> GetTournamentsByUserAsync(Guid id)
    {
        return await _userRepository.GetTournamentsByUserAsync(id);
    }

    public async Task<McResult<IEnumerable<User>>> GetAllUserAsync()
    {
        return await _userRepository.GetAllUserAsync();
    }
    
    public async Task<McResult<int>> GetWinsByUserAsync(Guid id)
    {
        return await _userRepository.GetWinsByUserAsync(id);
    }

    public async Task<McResult<int>> GetLosesByUserAsync(Guid id)
    {
        return await _userRepository.GetLosesByUserAsync(id);
    }

    public async Task<McResult<User>> GetUserByIdAsync(Guid id)
    {
        var user = await _context.Users
            .Include(u => u.Municipality)
            .Include(u => u.Roles)
            .ThenInclude(r => r.Claims)
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();
        
        if (user is null) return McResult<User>.Failure("User not found");
        return McResult<User>.Succeed(user);
    }

    public async Task<McResult<List<User>>> GetAllUsersAsync()
    {
        var users = await _context.Users
            .Include(u => u.Municipality)
            .Include(u => u.Roles)
            .ThenInclude(r => r.Claims)
            .ToListAsync();

        return McResult<List<User>>.Succeed(users);
    }

    public async Task<McResult<List<string>>> GetUserClaimsAsync(Guid id)
    {
        var user = await _context.Users
            .Include(u => u.Roles)
            .ThenInclude(r => r.Claims)
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();

        if (user is null)
        {
            return McResult<List<string>>.Failure("User not found");
        }
        
        var permissions = new List<string>();
        foreach (var role in user.Roles)
        {
            permissions.AddRange(role.Claims.Select(claim => claim.Name));
        }

        permissions = permissions.Distinct().ToList();
        return McResult<List<string>>.Succeed(permissions);
    }

    public async Task<McResult<string>> UpdateUserRole(Guid userId, UpdateUserRoleDto dto)
    {
        var userResponse = await GetUserByIdAsync(userId);
        if (userResponse.IsFailure)
        {
            return McResult<string>.Failure(userResponse.ErrorMessage, userResponse.ErrorCode);
        };
        var roleResponse = await _roleService.GetRoleByIdAsync(dto.RoleId);
        if (roleResponse.IsFailure)
        {
            return McResult<string>.Failure(roleResponse.ErrorMessage, roleResponse.ErrorCode);
        }
        
        var user = userResponse.Result;
        var role = roleResponse.Result;

        var userAlreadyHasRole = user.Roles.Any(r => r.Id == role.Id);
        if (dto.ToAdd)
        {
            if(userAlreadyHasRole) return McResult<string>.Failure("User already has this role", ErrorCodes.InvalidInput);
            user.Roles.Add(role);
        }
        else
        {
            if(!userAlreadyHasRole) return McResult<string>.Failure("User does not have this role", ErrorCodes.InvalidInput);
            user.Roles.Remove(role);
        }

        await _context.SaveChangesAsync();
        return McResult<string>.Succeed("Roles of user updated successfully");
    }
}