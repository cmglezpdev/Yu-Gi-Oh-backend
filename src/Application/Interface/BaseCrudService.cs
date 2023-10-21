using System.Linq.Expressions;
using AutoMapper;
using backend.Infraestructure.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace backend.Application.Interfaces;

public abstract class BaseCrudService<Entity, ModelDto> where Entity : PlatformModel where ModelDto : class
{
  protected readonly DbContext _context;
  protected readonly IMapper _mapper;

  public BaseCrudService(DbContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public virtual async Task<IEnumerable<Entity>> GetAllAsync(params Expression<Func<Entity, object>>[] relationships)
  {
    IQueryable<Entity> query = _context.Set<Entity>();
    foreach (var relationship in relationships)
    {
      query = query.Include(relationship);
    }

    return await query.ToListAsync();
  }

  public virtual async Task<Entity> GetByIdAsync(Guid Id, params Expression<Func<Entity, object>>[] relationships)
  {
    IQueryable<Entity> query = _context.Set<Entity>().Where(e => e.Id == Id);
    foreach (var relationship in relationships)
    {
      query = query.Include(relationship);
    }

    var item = await query.FirstOrDefaultAsync() ?? throw new BadHttpRequestException($"Item with id {Id} not found.");
    return item;
  }

  public virtual async Task<Entity> CreateAsync(ModelDto createEntityDto)
  {
    Entity entity = _mapper.Map<Entity>(createEntityDto);
    _context.Set<Entity>().Add(entity);
    await _context.SaveChangesAsync();
    return entity;
  }

  public virtual async Task<Entity> UpdateAsync(Guid Id, ModelDto updateEntityDto)
  {
    Entity entity = await GetByIdAsync(Id);
    _mapper.Map(updateEntityDto, entity);
    await _context.SaveChangesAsync();
    return entity;
  }

  public virtual async Task<Entity> DeleteAsync(Guid Id)
  {
    Entity entity = await GetByIdAsync(Id);
    _context.Set<Entity>().Remove(entity);
    await _context.SaveChangesAsync();
    return entity;
  }
}