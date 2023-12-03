using backend.Common.Enums;
using backend.Infrastructure;
using backend.Infrastructure.Common;
using backend.Infrastructure.Common.Enums;
using backend.Infrastructure.Entities;
using backend.Presentation.DTOs.Tournament;
using Microsoft.EntityFrameworkCore;

namespace backend.Application.Services;

public class TournamentsService
{
    private readonly AppDbContext _context;

    public TournamentsService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<McResult<IEnumerable<Tournament>>> FindAllTournaments()
    {
        var tournaments = await _context.Tournaments
            .Include(t => t.User)
            .Include(t => t.Municipality)
            .ToListAsync();
        return McResult<IEnumerable<Tournament>>.Succeed(tournaments);
    }
    
    public async Task<McResult<Tournament>> FindTournamentById(Guid Id)
    {
        var tournament = await _context.Tournaments.FirstOrDefaultAsync(t => t.Id == Id);
        return tournament is null 
            ? McResult<Tournament>.Failure($"Tournament with id {Id} not found", ErrorCodes.NotFound) 
            : McResult<Tournament>.Succeed(tournament);
    } 
    
    
    
    public async Task<McResult<string>> CreateTournament(TournamentInputDto input)
    {
        var Id = Guid.NewGuid();
        await _context.Tournaments.AddAsync(new Tournament()
        {
            Id = Id,
            Name = input.Name,
            Description = input.Description,
            UserId = input.UserId,
            MunicipalityId = input.MunicipalityId,
            StartDate = input.StartDate,
            EndDate = input.EndDate
        });
        await _context.SaveChangesAsync();

        return McResult<string>.Succeed("Tournament created successfully");
    }

    public async Task<McResult<User>> FindTournamentWinner(Guid tournamentId)
    {
        var exitsTournament = await _context.Tournaments
            .Where(t => t.Id == tournamentId)
            .AnyAsync();
        
        if(exitsTournament == false) return McResult<User>.Failure("The tournament not exits");
        
        // Get current round
        var currentRound = await _context.Duels
            .Where(d => d.TournamentId == tournamentId)
            .MaxAsync(d => d.Round);

        var duels = await _context.Duels
            .Where(d => d.TournamentId == tournamentId && d.Round == currentRound)
            .ToListAsync();

        if(duels.Count > 1 || duels[0].PlayerWinnerId is null) 
            return McResult<User>.Failure("The tournament has not finished yet");
        
        var user = await _context.Users.FirstAsync(u => u.Id == duels[0].PlayerWinnerId);
        return McResult<User>.Succeed(user);
    }

    public async Task<McResult<int>> GetManyParticipantsInTorunament(Guid tournamentId)
    {
        var tournaments = await _context.TournamentInscriptions
            .Where(t => (t.TournamentId == tournamentId && t.Status == InscriptionStatus.APPROVED))
            .ToListAsync();

        return McResult<int>.Succeed(tournaments.Count);
    }
}