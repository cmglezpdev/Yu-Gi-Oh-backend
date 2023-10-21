using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using backend.Infrastructure.Entities;
using backend.Application.Services;
using backend.Presentation.DTOs;
namespace backend.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProvinceController : ControllerBase
{
  private readonly ProvinceService _service;
  private readonly IMapper _mapper;
  public ProvinceController(ProvinceService service, IMapper mapper)
  {
    _service = service;
    _mapper = mapper;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<Province>>> GetAll()
  {
    var provinces = await _service.GetProvincesAsync();
    return Ok(_mapper.Map<IEnumerable<ProvinceOutputDto>>(provinces));
  }

  [HttpGet("{id:Guid}")]
  public async Task<ActionResult<Province>> GetById(Guid Id)
  {
    var province = await _service.GetProvinceByIdAsync(Id);
    return Ok(_mapper.Map<ProvinceOutputDto>(province));
  }
}