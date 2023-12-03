using AutoMapper;
using backend.Application.Repositories;
using backend.Infrastructure;
using backend.Infrastructure.Common;
using backend.Infrastructure.Entities;
using backend.Presentation.DTOs.Deck;
using Microsoft.EntityFrameworkCore;

namespace backend.Application.Services;
public class UserService
{
    private readonly AppDbContext _context;
    private readonly IDeckRepository _deckRepository;

    public UserService(AppDbContext context,IDeckRepository deckRepository)
    {
        _context = context;
        _deckRepository = deckRepository;
    }
    public async Task<IEnumerable<Deck>> GetDecksByUserAsync(Guid id)
    {
        return await _deckRepository.GetDecksByUserAsync(id);
    }

    public async Task<McResult<List<Tournament>>> GetTournamentsByUserAsync(Guid id)
    {
        var query = _context.Tournaments
            .Include(m => m.Municipality)
            .Where(t => t.UserId == id)
            .AsQueryable();

        var tournaments = await query.ToListAsync();
        return McResult<List<Tournament>>.Succeed(tournaments);
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
            Console.WriteLine("Llegó al error");
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
}