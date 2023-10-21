using Microsoft.AspNetCore.Mvc;
using backend.database;
namespace backend.localization;

public class MunicipalityController : BaseCrudController<Municipality, MunicipalityCreationDto>
{
  public MunicipalityController(MunicipalityService crudService) : base(crudService) { }

  // public override async Task<ActionResult<IEnumerable<Municipality>>> GetAll()
  // {
  // var items = await _service.GetAllAsync(e => e.Municipalities);
  // return Ok(items);
  // }
}