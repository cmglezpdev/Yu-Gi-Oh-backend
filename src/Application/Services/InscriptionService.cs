using backend.Common.Enums;
using backend.Infrastructure;
using backend.Infrastructure.Common;
using backend.Infrastructure.Common.Enums;
using backend.Infrastructure.Entities;
using backend.Presentation.DTOs.Inscriptions;
using backend.Presentation.DTOs.Tournament;
using Microsoft.EntityFrameworkCore;

namespace backend.Application.Services;

public class InscriptionService
{
    private readonly AppDbContext _context;

    public InscriptionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<McResult<IEnumerable<TournamentInscriptions>>> FindAllInscriptions(InscriptionFilterDto filter)
    {
        var query = _context.TournamentInscriptions
            .Include(i => i.Tournament)
            .Include(t => t.User)
            .Include(t => t.Deck)
            .Include(i =>  i.Tournament.Municipality)
            .AsQueryable();
        
        if(filter.TournamentId is not null) query = query.Where(ti => ti.TournamentId == filter.TournamentId);
        if(filter.Status is not null) query = query.Where(ti => ti.Status == filter.Status);
        if(filter.UserId is not null) query = query.Where(ti => ti.UserId == filter.UserId);
            
        var inscriptions = await query.ToListAsync();
        return McResult<IEnumerable<TournamentInscriptions>>.Succeed(inscriptions);
    }
    
    public async Task<McResult<TournamentInscriptions>> FindInscriptionById(Guid id)
    {
        var inscription = await _context.TournamentInscriptions
            .Include(i => i.User)
            .Include(i => i.Deck)
            .Include(i => i.Tournament)
            .FirstOrDefaultAsync(t => t.Id == id);
        return inscription is null 
            ? McResult<TournamentInscriptions>.Failure($"Inscription with id {id} not found", ErrorCodes.NotFound) 
            : McResult<TournamentInscriptions>.Succeed(inscription);
    } 
  
    public async Task<McResult<string>> CreateInscription(InscribeDto input)
    {
        var tournament = await _context.Tournaments.FirstOrDefaultAsync(t => t.Id == input.TournamentId);
        if (tournament is null) return McResult<string>.Failure($"Tournament with id {input.TournamentId} not found", ErrorCodes.NotFound);
        
        var user = await _context.Users.FirstOrDefaultAsync(p => p.Id == input.UserId);
        if (user is null) return McResult<string>.Failure($"User with id {input.UserId} not found", ErrorCodes.NotFound);

        var deck = await _context.Decks.FirstOrDefaultAsync(d => d.Id == input.DeckId && d.UserId == input.UserId);
        if (deck is null)
            return McResult<string>.Failure("The deck does not exist or does not belong to the user",
                ErrorCodes.NotFound);
        
        var exits = await _context.TournamentInscriptions
            .FirstOrDefaultAsync(ti => ti.UserId == input.UserId && ti.TournamentId == input.TournamentId);

        if (exits is not null)
        {
            return McResult<string>.Failure("The user is already registered in the tournament",
                ErrorCodes.OperationError);
        }

        await _context.TournamentInscriptions.AddAsync(new TournamentInscriptions()
        {
            UserId = input.UserId,
            DeckId = input.DeckId,
            TournamentId = input.TournamentId,
        });

        tournament.NumberOfInscriptions++;
        
        await _context.SaveChangesAsync();

        return McResult<string>.Succeed("The user was successfully registered in the tournament");
    }

    public async Task<McResult<string>> RejectInscription(Guid inscriptionId)
    {
        var inscription = await _context.TournamentInscriptions.FirstOrDefaultAsync(i => i.Id == inscriptionId);
        if (inscription is null) return McResult<string>.Failure($"Inscription with id {inscriptionId} not found", ErrorCodes.NotFound);
        if(inscription.Status == InscriptionStatus.REJECTED) return McResult<string>.Failure("The inscription is already rejected", ErrorCodes.OperationError);
        inscription.Status = InscriptionStatus.REJECTED;
        await _context.SaveChangesAsync();
        return McResult<string>.Succeed("The inscription was successfully rejected");
    }
    
    public async Task<McResult<string>> AcceptInscription(Guid inscriptionId)
    {
        var inscription = await _context.TournamentInscriptions.FirstOrDefaultAsync(i => i.Id == inscriptionId);
        if (inscription is null) return McResult<string>.Failure($"Inscription with id {inscriptionId} not found", ErrorCodes.NotFound);
        if(inscription.Status == InscriptionStatus.APPROVED) return McResult<string>.Failure("The inscription is already accepted", ErrorCodes.OperationError);

        var tournament = await _context.Tournaments.FirstAsync(t => t.Id == inscription.TournamentId); 
        if(DateTime.UtcNow >= tournament.StartDate) return McResult<string>.Failure("The tournament has already started", ErrorCodes.OperationError);
        
        inscription.Status = InscriptionStatus.APPROVED;
        tournament.NumberOfPlayers++;
        
        await _context.SaveChangesAsync();
        return McResult<string>.Succeed("The inscription was successfully accepted");
    }
}













