using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using backend.Infrastructure.Entities;
using backend.Application.Services;
using backend.Common.Authorization;
using backend.Infrastructure.Authentication;
using backend.Presentation.DTOs;
using backend.Presentation.DTOs.Card;

namespace backend.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CardController : ControllerBase
{
    private readonly CardService _service;
    private readonly IMapper _mapper;
    public CardController(CardService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }
    
    [HttpGet("{id:Guid}")]
    [HasPermission(Permission.ReadCard)]
    public async Task<ActionResult<Card>> GetByID(Guid Id)
    {
        var card = await _service.GetCardByIdAsync(Id);
        return Ok(_mapper.Map<CardOutputDto>(card));
    }
}