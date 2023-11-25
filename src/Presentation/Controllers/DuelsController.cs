using backend.Application.Services;
using backend.Presentation.DTOs.Duels;
using Microsoft.AspNetCore.Mvc;

namespace backend.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DuelsController : ControllerBase
{
    private readonly DuelsService _service;

    public DuelsController(DuelsService service)
    {
        _service = service;
    }

    [HttpPost("set-initial-duels")]
    public async Task<ActionResult> SetInitialDuels([FromBody] SetInitialDuelsDto dto)
    {
        var response = await _service.SetInitialDuels(dto.TournamentId);
        if (response.IsFailure) return BadRequest(response);
        return Ok(response);
    }
    
    [HttpPost("realize-duel")]
    public async Task<ActionResult> RealizeDuel([FromBody] RealizeDuelDto dto)
    {
        var response = await _service.RealizeDuel(dto.DuelId, dto.Winner);
        if (response.IsFailure) return BadRequest(response);
        return Ok(response);
    }
    
    [HttpPost("make-round-after-mixin")]
    public async Task<ActionResult> MakeNextRound([FromBody] MakeRoundAfterMixinDto afterMixinDto)
    {
        var response = await _service.MakeRoundAfterMixin(afterMixinDto.TournamentId, afterMixinDto.AmountOfPlayers);
        if (response.IsFailure) return BadRequest(response);
        return Ok(response);
    }
    
    [HttpPost("make-next-round")]
    public async Task<ActionResult> MakeNextRound([FromBody] MakeNextRoundDto nextRoundDto)
    {
        var response = await _service.MakeNextRound(nextRoundDto.TournamentId);
        if (response.IsFailure) return BadRequest(response);
        return Ok(response);
    }
}


































