using backend.localization;
using Microsoft.AspNetCore.Mvc;
namespace backend.database;

public class ProvinceController : BaseCrudController<Province, ProvinceCreationDto>
{
  public ProvinceController(ProvinceService crudService) : base(crudService) { }

  public override async Task<ActionResult<IEnumerable<Province>>> GetAll()
  {
    var items = await _service.GetAllAsync(e => e.Municipalities);
    return Ok(items);
  }

  public override async Task<ActionResult<Province>> GetById(Guid Id)
  {
    var item = await _service.GetByIdAsync(Id, e => e.Municipalities);
    return Ok(item);
  }
}