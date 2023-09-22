using AutoMapper;
using Microsoft.EntityFrameworkCore;
namespace backend.database;

public abstract class BaseCrudService<Entity, ModelDto> where Entity : PlatformModel where ModelDto : class
{
  protected readonly DbContext _context;
  protected readonly IMapper _mapper;

  public BaseCrudService(DbContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public async Task<IEnumerable<Entity>> GetAllAsync()
  {
    return await _context.Set<Entity>().ToListAsync();
  }

  public async Task<Entity> GetByIdAsync(Guid Id)
  {
    var item = await _context.Set<Entity>().FindAsync(Id) ?? throw new BadHttpRequestException($"Item with id {Id} not found.");
    return item;
  }

  public async Task<Entity> CreateAsync(ModelDto createEntityDto)
  {
    Entity entity = _mapper.Map<Entity>(createEntityDto);
    _context.Set<Entity>().Add(entity);
    await _context.SaveChangesAsync();
    return entity;
  }

  public async Task<Entity> UpdateAsync(Guid Id, ModelDto updateEntityDto)
  {
    Entity entity = await GetByIdAsync(Id);
    _mapper.Map(updateEntityDto, entity);
    await _context.SaveChangesAsync();
    return entity;
  }

  public async Task<Entity> DeleteAsync(Guid Id)
  {
    Entity entity = await GetByIdAsync(Id);
    _context.Set<Entity>().Remove(entity);
    await _context.SaveChangesAsync();
    return entity;
  }
}