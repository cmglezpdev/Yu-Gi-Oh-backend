using Microsoft.AspNetCore.Mvc;
using AutoMapper;

using backend.Application.Services;
using backend.Infrastructure.Entities;
using backend.Presentation.DTOs;
using backend.Presentation.DTOs.Municipality;

namespace backend.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MunicipalityController : ControllerBase
{
  private readonly MunicipalityService _service;
  private readonly IMapper _mapper;
  public MunicipalityController(MunicipalityService service, IMapper mapper)
  {
    _mapper = mapper;
    _service = service;
  }

  [HttpGet("{id:Guid}")]
  public async Task<ActionResult<Municipality>> GetById(Guid Id)
  {
    var municipality = await _service.GetMunicipalityByIdAsync(Id);
    return Ok(_mapper.Map<MunicipalityOutputDto>(municipality));
  }
}