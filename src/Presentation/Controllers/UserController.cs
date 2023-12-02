using AutoMapper;
using backend.Application.Services;
using backend.Infrastructure.Common;
using backend.Infrastructure.Entities;
using backend.Presentation.DTOs.Deck;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

[ApiController]    
[Route("api/[controller]")]
public class UserController : ControllerBase 
{
     private readonly UserService _service;
    private readonly IMapper _mapper;
  
    public UserController(UserService userService, IMapper mapper)
    {
        _service = userService;
        _mapper = mapper;
    }
    [HttpGet("decks/{id:Guid}")]
    public async Task<ActionResult> GetDeckByUser(Guid id)
    {
        var decks = await _service.GetDecksByUserAsync(id);
        return Ok(McResult<IEnumerable<DeckOutputDto>>.Succeed(
        _mapper.Map<IEnumerable<DeckOutputDto>>(decks))
        );
    }

    [HttpGet("tournaments/{id:Guid}")]
    public async Task<ActionResult> GetTournamentsByUser(Guid id)
    {
        var tournaments = await _service.GetTournamentsByUserAsync(id);
        if(tournaments.IsFailure) return BadRequest(tournaments);
        return Ok(tournaments);
    }
}