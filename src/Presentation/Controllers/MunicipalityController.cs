using backend.Presentation.Interfaces;
using backend.Infrastructure.Entities;
using backend.Presentation.DTOs;
using backend.Application.Services;
namespace backend.Presentation.Controllers;

public class MunicipalityController : BaseCrudController<Municipality, MunicipalityCreationDto>
{
  public MunicipalityController(MunicipalityService crudService) : base(crudService) { }

  // public override async Task<ActionResult<IEnumerable<Municipality>>> GetAll()
  // {
  // var items = await _service.GetAllAsync(e => e.Municipalities);
  // return Ok(items);
  // }
}