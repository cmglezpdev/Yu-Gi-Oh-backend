using AutoMapper;
using backend.Application.Services;
using backend.Infrastructure.Entities;
using backend.Presentation.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace backend.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArchetypeController : ControllerBase
{
    private readonly ArchetypeService _service;
    private readonly IMapper _mapper;
    public ArchetypeController(ArchetypeService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }
    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<Archetype>> GetById(Guid Id)
    {
        var municipality = await _service.GetArchetypeByIdAsync(Id);
        return Ok(_mapper.Map<ArchetypeOutputDto>(municipality));
    }
}