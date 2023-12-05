using backend.Infrastructure;
using backend.Infrastructure.Common;
using backend.Infrastructure.Common.Enums;
using backend.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace backend.Application.Services;
public class StatService
{
    private readonly AppDbContext _context;
    private readonly TournamentsService _tournamentService;
    public StatService(AppDbContext context, TournamentsService tournamentsService)
    {
        _context = context;
        _tournamentService = tournamentsService;
    }

    public async Task<McResult<IEnumerable<User>>> GetUserWithMoreDecks(int take)
    {
        var query = await _context.Decks
            .Include(u => u.User)
            .GroupBy(u => u.User )
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .Take(take)
            .ToListAsync();
        return McResult<IEnumerable<User>>.Succeed(query);
    }
    public async Task<McResult<IEnumerable<Archetype>>> GetMostPopularArchetype(int take)
    {
        var query = await _context.Decks
            .Include(a => a.Archetype)
            .GroupBy(a => a.Archetype)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .Take(take)
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
    public async Task<McResult<IEnumerable<User>>> GetUserWithMostWins(DateTime startDate, DateTime endDate, int take)
    {
        var query = await _context.Duels
            .Include(t => t.Tournament)
            .Include(u => u.PlayerWinner)
            .Where(t => t.Tournament.StartDate > startDate && t.Tournament.StartDate < endDate)
            .GroupBy(u => u.PlayerWinner)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .Take(take)
            .ToListAsync();
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
    public async Task<McResult<Municipality>> GetMunicipalityWithMoreWinners(DateTime startDate, DateTime endDate)
    {
        var championsID = this.GetWinners(startDate, endDate).Result.Result;
        if(championsID.Count() == 0) return McResult<Municipality>.Failure("No tournament has ended on that date");
        var query = await _context.Users
            .Include(m => m.Municipality)
            .Where(u => championsID.Contains(u.Id))
            .GroupBy(m => m.Municipality)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .FirstAsync();
        return McResult<Municipality>.Succeed(query);
    }
    public async Task<McResult<Archetype>> GetMostPopularArchetypeByTournamentAndRound(Guid tournamentId, int round)
    {
        var query = await _context.Duels
            .Where(d => d.TournamentId == tournamentId)
            .Join(_context.TournamentInscriptions,
                    a => a.Tournament,
                    b => b.Tournament,
                    (a,b) => new{
                        a.PlayerA,
                        a.PlayerB,
                        b.UserId,
                        b.Deck,
                        b.Deck.Archetype,
                        b.Status
                    })
            .Where(a => (a.UserId == a.PlayerA.Id || a.UserId == a.PlayerB.Id) && a.Status == InscriptionStatus.APPROVED)
            .GroupBy(g => g.Deck.Archetype)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .FirstAsync();
        return McResult<Archetype>.Succeed(query);
    }
    public async Task<McResult<IEnumerable<Archetype>>> GetArchetypeMoreUses(int take)
    {
        var query = await _context.TournamentInscriptions
            .Where(ti => ti.Status ==InscriptionStatus.APPROVED)
            .Include(d => d.Deck)
            .Include(a => a.Deck.Archetype)
            .GroupBy(a => a.Deck.Archetype)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Key)
            .Take(take)
            .ToListAsync();
        return McResult<IEnumerable<Archetype>>.Succeed(query);
    }
    public async Task<McResult<IEnumerable<dynamic>>> GetManyArchetypeWinner(DateTime startDate, DateTime endDate)
    {
        var championsID = this.GetWinners(startDate, endDate);
        if(championsID.Result.IsFailure) return McResult<IEnumerable<dynamic>>.Failure(championsID.Result.ErrorMessage);
        if(championsID.Result.Result.Count() == 0) return McResult<IEnumerable<dynamic>>.Failure("No tournament has ended on that date");
        var query = await _context.TournamentInscriptions
            .Include(d => d.Deck)
            .Include(a => a.Deck.Archetype)
            .Where(ti => championsID.Result.Result.Contains(ti.UserId))
            .GroupBy(t => t.Deck.Archetype)
            .Select(t => new{
                archetype = t.Key,
                count = t.Count()
            })
            .ToArrayAsync();
        return McResult<IEnumerable<dynamic>>.Succeed(query);
    }
    public async Task<McResult<IEnumerable<Guid>>> GetWinners(DateTime startDate, DateTime endDate)
    {
        var championsID = new List<Guid>();
        var tournamentId = await _context.Tournaments
                .Where(t => t.StartDate > startDate && t.StartDate < endDate)
                .Select(t => t.Id)
                .ToListAsync();
        if(tournamentId.Count() == 0) return McResult<IEnumerable<Guid>>.Failure("There are no tournaments on that date");

        foreach(var id in tournamentId)
        {
            var request = await _tournamentService.FindTournamentWinner(id);
            if(request.IsFailure) continue;
            else 
            {
                championsID.Add(request.Result.Id);
            }
        }
        return McResult<IEnumerable<Guid>>.Succeed(championsID);
    }
}