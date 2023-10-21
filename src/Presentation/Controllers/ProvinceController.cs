using Microsoft.AspNetCore.Mvc;
using backend.Infrastructure.Entities;
using backend.Application.Services;
namespace backend.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProvinceController : ControllerBase
{
  private readonly ProvinceService _service;
  public ProvinceController(ProvinceService service)
  {
    _service = service;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<Province>>> GetAll()
  {
    var provinces = await _service.GetProvincesAsync();
    return Ok(provinces);
  }

  [HttpGet("{id:Guid}")]
  public async Task<ActionResult<Province>> GetById(Guid Id)
  {
    var province = await _service.GetProvinceByIdAsync(Id);
    return Ok(province);
  }
}