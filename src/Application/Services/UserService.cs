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
    private readonly IDeckRepository deckRepository;

    public UserService(AppDbContext context,IMapper _mapper,IDeckRepository deckRepository)
    {
        _context = context;
        this.deckRepository = deckRepository;
    }
    public async Task<IEnumerable<Deck>> GetDecksByUserAsync(Guid id)
    {
        return await deckRepository.GetDecksByUserAsync(id);
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
}