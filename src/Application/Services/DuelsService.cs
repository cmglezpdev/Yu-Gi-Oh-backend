using backend.Common.Enums;
using backend.Infrastructure;
using backend.Infrastructure.Common;
using backend.Infrastructure.Common.Enums;
using backend.Infrastructure.Entities;
using backend.Presentation.DTOs.Duels;
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


    public async Task<McResult<List<DuelsEntity>>> GetDuels(FilterDuelDto filter)
    {
        var query = _context.Duels
            .Include(d => d.Tournament)
            .Include(d => d.PlayerA)
            .Include(d => d.PlayerB)
            .Include(d => d.PlayerWinner)
            .AsQueryable();

        if (filter.TournamentId is not null) query = query.Where(d => d.TournamentId == filter.TournamentId);
        if (filter.Round is not null) query = query.Where(d => d.Round == filter.Round);
            
        var duels = await query.ToListAsync();
        return McResult<List<DuelsEntity>>.Succeed(duels);
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
    public async Task<McResult<string>> MakeRoundAfterMixin(Guid tournamentId, int amountOfPlayers)
    {
        _logger.Log(LogLevel.Information, $"Creating the first round after initial mixin with the {amountOfPlayers} must winners players");
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
            .Where(d => d.PlayerWinnerId == null && d.TournamentId == tournamentId && d.Round == 0)
            .FirstOrDefaultAsync();

        if (incompleteDuels is not null)
        {
            _logger.Log(LogLevel.Error, "There are incomplete duels");
            return McResult<string>.Failure("There are incomplete duels", ErrorCodes.InvalidInput);
        }

        var allPlayers = await _context.TournamentInscriptions
            .Where(i => i.TournamentId == tournamentId && i.Status == InscriptionStatus.APPROVED)
            .ToListAsync();
        
        // Get the must winners players with at least one win 
        var winnerPlayers = await _context.Duels
            .Where(d => d.PlayerWinnerId != null && d.TournamentId == tournamentId && d.Round == 0)
            .GroupBy(d => d.PlayerWinnerId)
            .Select(group => new
            {
                PlayerId = group.Key,
                Wins = group.Count()
            })
            .OrderByDescending(result => result.Wins)
            .Take(amountOfPlayers)
            .ToListAsync();

        var players = new List<Guid>();
        for(int i = 0; i < winnerPlayers.Count; i++)
        {
            players.Add((Guid)winnerPlayers[i].PlayerId!);
        }

        for (int i = 0; i < allPlayers.Count && players.Count < amountOfPlayers; i++)
        {
            if(winnerPlayers.Any(wp => wp.PlayerId == allPlayers[i].UserId) == false)
                players.Add(allPlayers[i].UserId);
        }
        
        
        if (players.Count != amountOfPlayers)
        {
            _logger.Log(LogLevel.Information, "There are not enough players to create the round");
            return McResult<string>.Failure("There are not enough players to create the round", ErrorCodes.InvalidInput);
        }
        
        
        _logger.Log(LogLevel.Information, "Assigning duels randomly");
        var random = new Random();
        players = players.OrderBy(p => random.Next()).ToList();
            
        var duels = new List<DuelsEntity>();
        for (int i = 0; i < players.Count; i += 2)
        {
            duels.Add(new DuelsEntity(tournamentId, players[i], players[i + 1], 1));
        }
        
        await _context.Duels.AddRangeAsync(duels);
        await _context.SaveChangesAsync();
        
        _logger.Log(LogLevel.Information, "Round one created successfully");
        return McResult<string>.Succeed("Round one created successfully");
    }

    public async Task<McResult<string>> MakeNextRound(Guid tournamentId)
    {
        var currentRound = await _context.Duels
            .Where(d => d.TournamentId == tournamentId)
            .MaxAsync(d => d.Round);        
        
        _logger.Log(LogLevel.Information, $"Creating the round {currentRound + 1}");
        var duelsInLastRound = await _context.Duels
            .Where(d => d.TournamentId == tournamentId && d.Round == currentRound)
            .ToListAsync();

        if (duelsInLastRound.Any(d => d.PlayerWinnerId is null) == true)
        {
            _logger.Log(LogLevel.Error, "The current round is not finished");
            return McResult<string>.Failure("The current round is not finished", ErrorCodes.OperationError);
        }

        if (duelsInLastRound.Count == 1)
        {   
            _logger.Log(LogLevel.Information, "The tournament is finished");
            return McResult<string>.Succeed("The tournament is finished");
        }
        
        var players = new List<Guid>();
        foreach (var duel in duelsInLastRound)
        {
            players.Add((Guid)duel.PlayerWinnerId!);
        }
        
        _logger.Log(LogLevel.Information, "Assigning duels randomly");
        var random = new Random();
        players = players.OrderBy(p => random.Next()).ToList();
        
        var duels = new List<DuelsEntity>();
        for (int i = 0; i < players.Count; i += 2)
        {
            duels.Add(new DuelsEntity(tournamentId, players[i], players[i + 1], currentRound + 1));
        }
        
        await _context.Duels.AddRangeAsync(duels);
        await _context.SaveChangesAsync();
        
        _logger.Log(LogLevel.Information, $"Round {currentRound + 1} created successfully");
        return McResult<string>.Succeed($"Round {currentRound + 1} created successfully");
    }
}