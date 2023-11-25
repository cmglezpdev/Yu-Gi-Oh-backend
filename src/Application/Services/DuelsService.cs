using backend.Common.Enums;
using backend.Infrastructure;
using backend.Infrastructure.Common;
using backend.Infrastructure.Common.Enums;
using backend.Infrastructure.Entities;
using backend.Presentation.DTOs.Inscriptions;
using Microsoft.EntityFrameworkCore;

namespace backend.Application.Services;

public class DuelsService
{
    
    private readonly AppDbContext _context;
    private readonly InscriptionService _inscriptionService;
    private readonly ILogger<DuelsService> _logger;

    public DuelsService(AppDbContext context, InscriptionService inscriptionService, ILogger<DuelsService> logger)
    {
        _context = context;
        _inscriptionService = inscriptionService;
        _logger = logger;
    }


    public async Task<McResult<string>> SetInitialDuels(Guid tournamentId)
    {
        var playersResponse = await _inscriptionService.FindAllInscriptions(new InscriptionFilterDto()
        {
            TournamentId = tournamentId,
            Status = InscriptionStatus.APPROVED
        });
        
        if(playersResponse.IsFailure) return McResult<string>.Failure(playersResponse.ErrorMessage, playersResponse.ErrorCode);
        var players = playersResponse.Result.ToList();
        var duels = new List<DuelsEntity>();
        
        for (int i = 0; i < players.Count; i++)
        {
            for (int j = i + 1; j < players.Count; j++)
            {
                duels.Add(new DuelsEntity(tournamentId, players[i].UserId, players[j].UserId));
            }
        }
        
        await _context.Duels.AddRangeAsync(duels);
        await _context.SaveChangesAsync();
        
        return McResult<string>.Succeed("Initial duels created successfully");
    }
    
    public async Task<McResult<string>> RealizeDuel(Guid duelId, char winner)
    {
        var duel = await _context.Duels.FirstOrDefaultAsync(d => d.Id == duelId);
        if(duel is null) return McResult<string>.Failure($"Duel with id {duelId} not found", ErrorCodes.NotFound);
        
        var result = duel.RealizeDuel(winner);
        if(result.IsFailure) return McResult<string>.Failure(result.ErrorMessage, result.ErrorCode);
        
        await _context.SaveChangesAsync();
        return McResult<string>.Succeed("Duel realized successfully");
    }
    
    // When the zero round is completed, then next round is created with the K must winners players
    public async Task<McResult<string>> MakeNextRound(Guid tournamentId, int currentRound, int amountOfPlayers)
    {
        _logger.Log(LogLevel.Information, $"Creating the second round with the {amountOfPlayers} must winners players");
        // amountOfPlayers must be a power of 2
        int k = amountOfPlayers;
        while (k > 1 && k % 2 == 0) k /= 2;
        if (k != 1)
        {
            _logger.Log(LogLevel.Error, $"Amount of players must be a power of 2");
            return McResult<string>.Failure("Amount of players must be a power of 2", ErrorCodes.InvalidInput);
        }
        
        // There are no incomplete duels
        var incompleteDuels = await _context.Duels
            .Where(d => d.PlayerWinner == null && d.TournamentId == tournamentId && d.Round == currentRound)
            .FirstOrDefaultAsync();

        if (incompleteDuels is not null)
        {
            _logger.Log(LogLevel.Error, "There are incomplete duels");
            return McResult<string>.Failure("There are incomplete duels", ErrorCodes.InvalidInput);
        }
        
        // Get the amountOfPlayers must winners players 
        var players = await _context.Duels
            .Where(d => d.PlayerWinner != null && d.TournamentId == tournamentId && d.Round == currentRound)
            .GroupBy(d => d.PlayerWinner)
            .Select(group => new
            {
                PlayerId = group.Key,
                Wins = group.Count()
            })
            .OrderByDescending(result => result.Wins)
            .Take(amountOfPlayers)
            .ToListAsync();

        if (players.Count != amountOfPlayers)
        {
            _logger.Log(LogLevel.Information, "There are not enough players to create the second round");
            return McResult<string>.Failure("There are not enough players to create the second round", ErrorCodes.InvalidInput);
        }
        
        
        _logger.Log(LogLevel.Information, "Assigning duels randomly");
        var random = new Random();
        players = players.OrderBy(p => random.Next()).ToList();
            
        var duels = new List<DuelsEntity>();
        for (int i = 0; i < players.Count; i += 2)
        {
            duels.Add(new DuelsEntity(
                tournamentId,
                (Guid)players[i].PlayerId!,
                (Guid)players[i + 1].PlayerId!,
                currentRound + 1));
        }
        
        await _context.Duels.AddRangeAsync(duels);
        await _context.SaveChangesAsync();
        
        _logger.Log(LogLevel.Information, $"Round {currentRound + 1} created successfully");
        return McResult<string>.Succeed($"Round {currentRound + 1} created successfully");
    }
    
}