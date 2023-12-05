using AutoMapper;
using backend.Application.Services;
using backend.Common.Authorization;
using backend.Infrastructure.Authentication;
using backend.Infrastructure.Common;
using backend.Infrastructure.Entities;
using backend.Presentation.DTOs.Deck;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Presentation.Controllers;

[ApiController]    
[Route("api/[controller]")]
public class DecksController : ControllerBase 
{
  private readonly DeckService _service;
  private readonly IMapper _mapper;
  
  public DecksController(DeckService deckService, IMapper mapper)
  {
      _service = deckService;
      _mapper = mapper;
  }
  
  [HttpGet("{id:Guid}")]
  [HasPermission(Permission.ReadDeck)]
  public async Task<ActionResult> GetDeckByIdAsync(Guid id)
  {
    var deck = await _service.GetDeckByIdAsync(id);
    return Ok(McResult<Deck>.Succeed(deck));
  }

  [HttpPost]
  [HasPermission(Permission.WriteDeck)]
  public async Task<ActionResult<Deck>> CreateDeckAsync(DeckInputDto dto)
  {
    var deck = await _service.CreateDeckAsync(dto);
    return Ok(_mapper.Map<DeckOutputDto>(deck));
  }

  [HttpPut("{id:Guid}")]
  [HasPermission(Permission.WriteDeck)]
  public async Task<ActionResult<Deck>> UpdateDeckAsync(Guid id, DeckInputDto dto)
  {
      var deck = await _service.UpdateDeckAsync(id, dto);
      return Ok(_mapper.Map<DeckOutputDto>(deck));
  }

  [HttpDelete("{id:Guid}")]
  [HasPermission(Permission.WriteDeck)]
  public async Task<ActionResult<Deck>> DeleteDeck(Guid id)
  {
    var deck = await _service.DeleteDeckById(id);
    return Ok(_mapper.Map<DeckOutputDto>(deck));
  }
}
