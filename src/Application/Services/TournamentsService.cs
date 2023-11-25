using backend.Common.Enums;
using backend.Infrastructure;
using backend.Infrastructure.Common;
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
        var tournaments = await _context.Tournaments.ToListAsync();
        return McResult<IEnumerable<Tournament>>.Succeed(tournaments);
    }
    
    public async Task<McResult<Tournament>> FindTournamentById(Guid Id)
    {
        var tournament = await _context.Tournaments.FirstOrDefaultAsync(t => t.Id == Id);
        return tournament is null 
            ? McResult<Tournament>.Failure($"Tournament with id {Id} not found", ErrorCodes.NotFound) 
            : McResult<Tournament>.Succeed(tournament);
    } 
    
    public async Task<McResult<Tournament>> CreateTournament(TournamentInputDto input)
    {
        var Id = Guid.NewGuid();
        await _context.Tournaments.AddAsync(new Tournament()
        {
            Id = Id,
            Name = input.Name,
            Description = input.Description,
            MunicipalityId = input.MunicipalityId,
            StartDate = input.StartDate,
            EndDate = input.EndDate
        });
        await _context.SaveChangesAsync();

        var tournament = await _context.Tournaments
            .FirstAsync(t => t.Id == Id);
        return McResult<Tournament>.Succeed(tournament);
    }

    public async Task<McResult<string>> InscribePlayer(Guid tournamentId, InscribeDto input)
    {
        var tournament = await _context.Tournaments.FirstOrDefaultAsync(t => t.Id == tournamentId);
        if (tournament is null) return McResult<string>.Failure($"Tournament with id {tournamentId} not found", ErrorCodes.NotFound);
        
        var user = await _context.Users.FirstOrDefaultAsync(p => p.Id == input.UserId);
        if (user is null) return McResult<string>.Failure($"User with id {input.UserId} not found", ErrorCodes.NotFound);

        var deck = await _context.Decks.FirstOrDefaultAsync(d => d.Id == input.DeckId && d.UserId == input.UserId);
        if (deck is null)
            return McResult<string>.Failure("The deck does not exist or does not belong to the user",
                ErrorCodes.NotFound);
        
        var exits = await _context.TournamentInscriptions
            .FirstOrDefaultAsync(ti => ti.UserId == input.UserId && ti.TournamentId == tournamentId);

        if (exits is not null)
        {
            return McResult<string>.Failure("The user is already registered in the tournament",
                ErrorCodes.OperationError);
        }

        await _context.TournamentInscriptions.AddAsync(new TournamentInscriptions()
        {
            UserId = input.UserId,
            DeckId = input.DeckId,
            TournamentId = tournamentId,
            IsApproved = false
        });
        
        await _context.SaveChangesAsync();

        return McResult<string>.Succeed("The user was successfully registered in the tournament");
    }
}