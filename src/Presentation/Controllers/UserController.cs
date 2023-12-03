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

    [HttpGet]
    public async Task<ActionResult> GetAllUser()
    {
        var users = await _service.GetAllUserAsync();
        if(users.IsFailure) return BadRequest(users);
        return Ok(users);
    }

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult> GetUserById(Guid id)
    {
        var user = await _service.GetUserByIdAsync(id);
        if(user.IsFailure) return BadRequest(user);
        return Ok(user);
    }

    [HttpGet("decks/{id:Guid}")]
    public async Task<ActionResult> GetDeckByUser(Guid id)
    {
        var decks = await _service.GetDecksByUserAsync(id);
        if(decks.IsFailure) return BadRequest(decks);
        return Ok((decks));
    }

    [HttpGet("tournaments/{id:Guid}")]
    public async Task<ActionResult> GetTournamentsByUser(Guid id)
    {
        var tournaments = await _service.GetTournamentsByUserAsync(id);
        if(tournaments.IsFailure) return BadRequest(tournaments);
        return Ok(tournaments);
    }

    [HttpGet("wins/{id:Guid}")]
    public async Task<ActionResult> GetWinsByUser(Guid id)
    {
        var wins = await _service.GetWinsByUserAsync(id);
        if(wins.IsFailure) return BadRequest(wins);
        return Ok(wins);
    }

    [HttpGet("loses/{id:Guid}")]
    public async Task<ActionResult> GetLosesByUser(Guid id)
    {
        var loses = await _service.GetLosesByUserAsync(id);
        if(loses.IsFailure) return BadRequest(loses);
        return Ok(loses);
    }
}
