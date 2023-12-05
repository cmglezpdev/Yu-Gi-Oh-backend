using backend.Application.Repositories;
using backend.Application.Services;
using backend.Common.Enums;
using backend.Infrastructure.Common;
using backend.Infrastructure.Entities;
using backend.Presentation.DTOs.User;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    private readonly RoleService _roleService;

    public UserRepository(AppDbContext context, RoleService roleService)
    {
        _context = context;
        _roleService = roleService;
    }
    
    public async Task<McResult<User>> GetUserByIdAsync(Guid id)
    {
        try
        {
            var user = await _context.Users
                .Include(u => u.Municipality)
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Id == id);
            return user is not null 
                ? McResult<User>.Succeed(user) 
                : McResult<User>.Failure($"User with id {id} not found", ErrorCodes.NotFound);
        }
        catch (Exception e)
        {
            return McResult<User>.Failure(e.Message);
        }
    }

    public async Task<McResult<User>> GetUserByEmailAsync(string email)
    {
        try
        {
            var user = await _context.Users
                .Include(u => u.Municipality)
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Email == email);
            return user is not null 
                ? McResult<User>.Succeed(user) 
                : McResult<User>.Failure($"User with email {email} not found", ErrorCodes.NotFound);
        }
        catch (Exception e)
        {
            return McResult<User>.Failure(e.Message);
        }
    }

    public async Task<McResult<User>> GetUserByUserNameAsync(string userName)
    {
        try
        {
            var user = await _context.Users
                .Include(u => u.Municipality)
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.UserName == userName);
            return user is not null 
                ? McResult<User>.Succeed(user) 
                : McResult<User>.Failure($"User with username {userName} not found", ErrorCodes.NotFound);
        }
        catch (Exception e)
        {
            return McResult<User>.Failure(e.Message);
        }
    }

    public async Task<McResult<User>> CreateUserAsync(UserInputDto user)
    {
        var role = await _roleService.GetRoleByNameAsync(RolesType.Player.ToString());
        if (role.IsFailure) return McResult<User>.Failure(role.ErrorMessage, role.ErrorCode);
        
        try
        {
            await _context.Users.AddAsync(new User()
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Password = user.Password,
                Name = user.Name,
                MunicipalityId = user.MunicipalityId,
                Roles = new List<RoleEntity>(){ role.Result }
            });
            await _context.SaveChangesAsync();
            var userPersistent = await _context.Users
                .Include(u => u.Municipality)
                .Include(u => u.Roles)
                .ThenInclude(u => u.Claims)
                .FirstAsync(u => u.Id == user.Id);
            return McResult<User>.Succeed(userPersistent);
        }
        catch (Exception e)
        {
            return McResult<User>.Failure(e.Message);
        }
    }
    
    public async Task<McResult<User>> UpdateUserAsync(UserUpdateDto user)
    {
        // _context.Users.Update(new User()
        // {
        //     Id = user.Id,
        //     Name = user.Name,
        // });
        // await _context.SaveChangesAsync();
        var userPersistent = await _context.Users
            .Include(u => u.Municipality)
            .Include(u => u.Roles)
            .FirstAsync(u => u.Id == user.Id);
        return McResult<User>.Succeed(userPersistent);
    }

    public async Task<McResult<IEnumerable<Deck>>> GetDecksByUserAsync(Guid id)
    {
        IQueryable<Deck> query = _context.Set<Deck>()
            .Include(a => a.Archetype)
            .Where(d => d.UserId == id);
        
        var decks = await query.ToListAsync();
        return McResult<IEnumerable<Deck>>.Succeed(decks);
    }

    public async Task<McResult<IEnumerable<Tournament>>> GetTournamentsByUserAsync(Guid id)
    {
        var query = _context.Tournaments
            .Include(m => m.Municipality)
            .Where(t => t.UserId == id)
            .AsQueryable();

        var tournaments = await query.ToListAsync();
        return McResult<IEnumerable<Tournament>>.Succeed(tournaments);
    }

    public async Task<McResult<IEnumerable<User>>> GetAllUserAsync()
    {
        var query = _context.Users
            .Include(m => m.Municipality)
            .ThenInclude(m => m.Province);

        var users = await query.ToListAsync();
        return McResult<IEnumerable<User>>.Succeed(users);
    }

    public async Task<McResult<int>> GetWinsByUserAsync(Guid id)
    {
        var query = _context.Duels
            .Count(w => w.PlayerWinnerId == id);

        return McResult<int>.Succeed(query);
    }

    public async Task<McResult<int>> GetLosesByUserAsync(Guid id)
    {
        var query = _context.Duels
            .Count(w => (w.PlayerAId == id || w.PlayerBId == id) && (w.PlayerWinnerId != id));

        return McResult<int>.Succeed(query);
    }
}
