using backend.Application.Services;
using backend.Presentation.DTOs.Duels;
using Microsoft.AspNetCore.Mvc;

namespace backend.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatisticsController : ControllerBase
{
    private readonly StatService _service;

    public StatisticsController(StatService service)
    {
        _service = service;
    }

    [HttpGet("decks/{take:int}")]
    public async Task<ActionResult> GetUserWithMoreDecks(int take)
    {
        var request = await _service.GetUserWithMoreDecks(take);
        if(request.IsFailure) return BadRequest(request);
        return Ok(request);
    }

    [HttpGet("archetype/popular/{take:int}")]
    public async Task<ActionResult> GetMostPopularArchetype(int take)
    {
        var request = await _service.GetMostPopularArchetype(take);
        if(request.IsFailure) return BadRequest(request);
        return Ok(request);
    }

    [HttpGet("municipality/{archetypeId:Guid}")]
    public async Task<ActionResult> GetMostPopularMunicipalityByArchetype(Guid archetypeId)
    {
        var request = await _service.GetMostPopularMunicipalityByArchetype(archetypeId);
        if(request.IsFailure) return BadRequest(request);
        return Ok(request);
    }

    [HttpGet("tournament/{tournamentId:Guid}")]
    public async Task<ActionResult> GetMostPopularArchetypeByTournament(Guid tournamentId)
    {
        var request = await _service.GetMostPopularArchetypeByTournament(tournamentId);
        if(request.IsFailure) return BadRequest(request);
        return Ok(request);
    }

    [HttpGet("archetype/{take:int}")]
    public async Task<ActionResult> GetArchetypeMoreUses(int take)
    {
        var request = await _service.GetArchetypeMoreUses(take);
        if(request.IsFailure) return BadRequest(request);
        return Ok(request);
    }

    [HttpGet("archetype/{tournamentId:Guid}/{round:int}")]
    public async Task<ActionResult> GetMostPopularArchetypeByTournamentAndRound(Guid tournamentId, int round)
    {
        var request = await _service.GetMostPopularArchetypeByTournamentAndRound(tournamentId, round);
        if(request.IsFailure) return BadRequest(request);
        return Ok(request);
    }

    [HttpGet("user/{startDate:DateTime}/{endDate:DateTime}/{take:int}")]
    public async Task<ActionResult> GetUserWithMostWins(DateTime startDate, DateTime endDate, int take)
    {  
        var request = await _service.GetUserWithMostWins(DateTime.SpecifyKind(startDate, DateTimeKind.Utc), DateTime.SpecifyKind(endDate, DateTimeKind.Utc), take);
        if(request.IsFailure) return BadRequest(request);
        return Ok(request);
    }

    [HttpGet("archetype/winner/{startDate:DateTime}/{endDate:DateTime}")]
    public async Task<ActionResult> GetManyArchetypeWinner(DateTime startDate, DateTime endDate)
    {
        var request = await _service.GetManyArchetypeWinner(DateTime.SpecifyKind(startDate, DateTimeKind.Utc), DateTime.SpecifyKind(endDate, DateTimeKind.Utc));
        if(request.IsFailure) return BadRequest(request);
        return Ok(request);
    }

    [HttpGet("municipality/{startDate:DateTime}/{endDate:DateTime}/")]
    public async Task<ActionResult> GetMunicipalityWithMoreWinners(DateTime startDate, DateTime endDate)
    {  
        var request = await _service.GetMunicipalityWithMoreWinners(DateTime.SpecifyKind(startDate, DateTimeKind.Utc), DateTime.SpecifyKind(endDate, DateTimeKind.Utc));
        if(request.IsFailure) return BadRequest(request);
        return Ok(request);
    }
}