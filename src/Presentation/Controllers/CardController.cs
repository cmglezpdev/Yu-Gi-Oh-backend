using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using backend.Infrastructure.Entities;
using backend.Application.Services;
using backend.Presentation.DTOs;
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
    public async Task<ActionResult<Card>> GetByID(Guid Id)
    {
        var card = await _service.GetCardByIdAsync(Id);
        return Ok(_mapper.Map<CardOutputDto>(card));
    }
}