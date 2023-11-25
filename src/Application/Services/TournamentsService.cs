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
    
    public async Task<McResult<string>> CreateTournament(TournamentInputDto input)
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

        return McResult<string>.Succeed("Tournament created successfully");
    }
}