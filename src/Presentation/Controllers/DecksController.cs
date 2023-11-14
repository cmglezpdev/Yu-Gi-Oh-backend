using backend.Presentation.DTOs;
using backend.Infrastructure.Entities;
using backend.Application.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using backend.Application.Repositories;

namespace backend.Application.Controllers;

[ApiController]    
[Route("api/[controller]")]
public class DecksController : ControllerBase {

  private readonly DeckService _service;

  private readonly IMapper _mapper;
  public DecksController(DeckService deckService, IMapper mapper)
  {
      _service = deckService;
      _mapper = mapper;
  }
  
  [HttpGet]
  public async Task<ActionResult<Deck>> GetDeckAll()
  {
    var decks = await _service.GetAllAsync();
    return Ok(decks);
  }

  [HttpPost]
  public async Task<ActionResult<Deck>> PostDeck(DeckInputDto dto)
  {
      var deck = await _service.PostDeck(dto);
      return Ok(_mapper.Map<DeckOutputDto>(deck));
  }
}
