using backend.Application.Repositories;
using backend.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
namespace backend.Infrastructure.Repositories;

public class ProvinceRepository : IProvinceRepository
{
  private readonly AppDbContext _context;
  public ProvinceRepository(AppDbContext context)
  {
    _context = context;
  }

  public async Task<Province> GetProvinceByIdAsync(Guid Id)
  {
    IQueryable<Province> query = _context.Set<Province>()
      .Where(p => p.Id == Id)
      .Include(p => p.Municipalities);

    return await query.FirstOrDefaultAsync() ?? throw new BadHttpRequestException($"Province with id {Id} not found.");
  }

  public async Task<IEnumerable<Province>> GetProvincesAsync()
  {
    IQueryable<Province> query = _context.Set<Province>().Include(p => p.Municipalities);
    return await query.ToListAsync();
  }
}