using AutoMapper;
using backend.Application.Services;
using backend.Common.Authorization;
using backend.Infrastructure.Authentication;
using backend.Infrastructure.Common;
using backend.Presentation.DTOs.Deck;
using backend.Presentation.DTOs.User;
using Microsoft.AspNetCore.Mvc;

namespace backend.Presentation.Controllers;

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
    [HasPermission(Permission.ReadUser)]
    public async Task<ActionResult> GetAllUsers()
    {
        var users = await _service.GetAllUsersAsync();
        if(users.IsFailure) return BadRequest(users);
        return Ok(users);
    }
    
    [HttpGet("{id:Guid}")]
    [HasPermission(Permission.ReadDeck)]
    public async Task<ActionResult> GetUserById(Guid id)
    {
        var user = await _service.GetUserByIdAsync(id);
        if(user.IsFailure) return BadRequest(user);
        return Ok(user);
    }
    
    [HttpGet("decks/{id:Guid}")]
    [HasPermission(Permission.ReadDeck)]
    public async Task<ActionResult> GetDeckByUser(Guid id)
    {
        var decks = await _service.GetDecksByUserAsync(id);
        if(decks.IsFailure) return BadRequest(decks);
        return Ok((decks));
    }

    [HttpGet("tournaments/{id:Guid}")]
    [HasPermission(Permission.ReadTournament)]
    public async Task<ActionResult> GetTournamentsByUser(Guid id)
    {
        var tournaments = await _service.GetTournamentsByUserAsync(id);
        if(tournaments.IsFailure) return BadRequest(tournaments);
        return Ok(tournaments);
    }

    [HttpPatch("{userId:Guid}/update-role")]
    [HasPermission(Permission.WriteUser)]
    public async Task<ActionResult> UpdateUserRole(Guid userId, [FromBody] UpdateUserRoleDto dto)
    {
        var response = await _service.UpdateUserRole(userId, dto);
        if(response.IsFailure) return BadRequest(response);
        return Ok(response);
    }

    [HttpGet("wins/{id:Guid}")]
    [HasPermission(Permission.ReadTournament)]
    public async Task<ActionResult> GetWinsByUser(Guid id)
    {
        var wins = await _service.GetWinsByUserAsync(id);
        if(wins.IsFailure) return BadRequest(wins);
        return Ok(wins);
    }

    [HttpGet("loses/{id:Guid}")]
    [HasPermission(Permission.ReadTournament)]
    public async Task<ActionResult> GetLosesByUser(Guid id)
    {
        var loses = await _service.GetLosesByUserAsync(id);
        if(loses.IsFailure) return BadRequest(loses);
        return Ok(loses);
    }
}
