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

    [HttpGet("decks")]
    public async Task<ActionResult> GetUserWithMoreDecks()
    {
        var request = await _service.GetUserWithMoreDecks();
        if(request.IsFailure) return BadRequest(request);
        return Ok(request);
    }

    [HttpGet("archetype")]
    public async Task<ActionResult> GetMostPopularArchetype()
    {
        var request = await _service.GetMostPopularArchetype();
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

    [HttpGet("archetype/{count:int}")]
    public async Task<ActionResult> GetArchetypeMoreUses(int count)
    {
        var request = await _service.GetArchetypeMoreUses(count);
        if(request.IsFailure) return BadRequest(request);
        return Ok(request);
    }
}