using backend.Application.Interface;
using backend.Infrastructure.Common;
using Microsoft.AspNetCore.Mvc;

namespace backend.Presentation.Interfaces;

[ApiController]
// [Route("api/[controller]")]
public abstract class BaseCrudController<Entity, ModelDto> : ControllerBase where Entity : PlatformModel where ModelDto : class
{
  protected readonly BaseCrudService<Entity, ModelDto> _service;

  public BaseCrudController(BaseCrudService<Entity, ModelDto> crudService)
  {
    _service = crudService;
  }

  [HttpGet]
  public virtual async Task<ActionResult<IEnumerable<Entity>>> GetAll()
  {
    var items = await _service.GetAllAsync();
    return Ok(items);
  }

  [HttpGet("{id:Guid}")]
  public virtual async Task<ActionResult<Entity>> GetById(Guid Id)
  {
    var item = await _service.GetByIdAsync(Id);
    return Ok(item);
  }

  [HttpPost]
  public virtual async Task<ActionResult<Entity>> Create([FromBody] ModelDto createEntityDto)
  {
    var item = await _service.CreateAsync(createEntityDto);
    return Ok(item);
  }

  [HttpPut("{id:Guid}")]
  public virtual async Task<ActionResult<Entity>> Update(Guid Id, [FromBody] ModelDto updateEntityDto)
  {
    var item = await _service.UpdateAsync(Id, updateEntityDto);
    return Ok(item);
  }

  [HttpDelete("{id:Guid}")]
  public virtual async Task<ActionResult<Entity>> Delete(Guid Id)
  {
    var item = await _service.DeleteAsync(Id);
    return Ok(item);
  }
}