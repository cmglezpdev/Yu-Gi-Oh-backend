using backend.Application.Services;
using backend.Infrastructure.Entities;
using backend.Presentation.DTOs.Tournament;
using Microsoft.AspNetCore.Mvc;

namespace backend.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TournamentController : ControllerBase
{
    private readonly TournamentsService _tournamentsService;

    public TournamentController(TournamentsService tournamentsService)
    {
        _tournamentsService = tournamentsService;
    }

    [HttpGet("date")]
    public ActionResult GetDate()
    {
        return Ok(DateTime.UtcNow);
    }
    
    [HttpGet]
    public async Task<ActionResult> FindAllTournaments()
    {
        var response = await _tournamentsService.FindAllTournaments();
        if(response.IsFailure) return BadRequest(response);
        return Ok(response);
    }
    
    [HttpGet("{id:Guid}")]
    public async Task<ActionResult> FindTournamentById(Guid id)
    {
        var response = await _tournamentsService.FindTournamentById(id);
        if(response.IsFailure) return BadRequest(response);
        return Ok(response);
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateTournament([FromBody] TournamentInputDto input)
    {
        var response = await _tournamentsService.CreateTournament(input);
        if(response.IsFailure) return BadRequest(response);
        return Ok(response);
    }
}