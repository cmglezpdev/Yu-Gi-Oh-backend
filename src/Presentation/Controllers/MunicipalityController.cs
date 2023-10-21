using Microsoft.AspNetCore.Mvc;
using backend.Application.Services;
using backend.Infrastructure.Entities;
namespace backend.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MunicipalityController : ControllerBase
{
  private readonly MunicipalityService _service;
  public MunicipalityController(MunicipalityService service)
  {
    _service = service;
  }

  [HttpGet("{id:Guid}")]
  public async Task<ActionResult<Municipality>> GetById(Guid Id)
  {
    var municipality = await _service.GetMunicipalityByIdAsync(Id);
    return Ok(municipality);
  }
}