using backend.Application.Repositories;
using backend.Infrastructure.Common;
using backend.Infrastructure.Common.Enums;
using backend.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
namespace backend.Infrastructure.Repositories;

public class StatRepository : IStatRepository
{
    private readonly AppDbContext _context;
    public StatRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<McResult<IEnumerable<User>>> GetUserWithMoreDecks()
    {
        var query = await _context.Decks
            .Include(u => u.User)
            .GroupBy(u => u.User )
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .ToListAsync();
        return McResult<IEnumerable<User>>.Succeed(query);
    }
    public async Task<McResult<IEnumerable<Archetype>>> GetMostPopularArchetype()
    {
        var query = await _context.Decks
            .Include(a => a.Archetype)
            .GroupBy(a => a.Archetype)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .ToListAsync();
        return McResult<IEnumerable<Archetype>>.Succeed(query);
    }
    public async Task<McResult<Municipality>> GetMostPopularMunicipalityByArchetype(Guid archetypeId)
    {
        var query = _context.Decks
            .Include(a => a.Archetype)
            .Include(u => u.User)
            .Include(m => m.User.Municipality)
            .Where(a => a.ArchetypeId == archetypeId)
            .Select(n => new {
                n.User.Municipality,
                n.ArchetypeId
            })
            .GroupBy(a => a.Municipality)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .First();

        return McResult<Municipality>.Succeed(query);
    }
    public async Task<McResult<IEnumerable<User>>> GetUserWithMostWins()
    {
        var query = await _context.Users.ToListAsync();
        return McResult<IEnumerable<User>>.Succeed(query);
    }
    public async Task<McResult<Archetype>> GetMostPopularArchetypeByTournament(Guid tournamentId)
    {
        var query = await _context.TournamentInscriptions
            .Where(ti => ti.TournamentId == tournamentId && ti.Status == InscriptionStatus.APPROVED)
            .Include(d => d.Deck)
            .Include(a => a.Deck.Archetype)
            .GroupBy(a => a.Deck.Archetype)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .FirstAsync();
        return McResult<Archetype>.Succeed(query);
    }
    public async Task<McResult<Municipality>> GetMunicipalityWithMoreWinners()
    {
        var query = await _context.Municipalities.FirstAsync();
        return McResult<Municipality>.Succeed(query);
    }
    public async Task<McResult<IEnumerable<Archetype>>> GetMostPopularArchetypeByTournament(Guid tournamentId, int round)
    {
        var query = await _context.Archetypes.ToListAsync();
        return McResult<IEnumerable<Archetype>>.Succeed(query);
    }
    public async Task<McResult<IEnumerable<Archetype>>> GetArchetypeMoreUses(int count)
    {
        var query = await _context.TournamentInscriptions
            .Where(ti => ti.Status ==InscriptionStatus.APPROVED)
            .Include(d => d.Deck)
            .Include(a => a.Deck.Archetype)
            .GroupBy(a => a.Deck.Archetype)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .Take(count)
            .ToListAsync();
        return McResult<IEnumerable<Archetype>>.Succeed(query);
    }
}