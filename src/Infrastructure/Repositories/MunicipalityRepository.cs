using backend.Application.Repositories;
using backend.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
namespace backend.Infrastructure.Repositories;

public class MunicipalityRepository : IMunicipalityRepository
{
  private readonly AppDbContext _context;
  public MunicipalityRepository(AppDbContext context)
  {
    _context = context;
  }

  public async Task<Municipality> GetMunicipalityByIdAsync(Guid Id)
  {
    IQueryable<Municipality> query = _context.Set<Municipality>()
      .Where(m => m.Id == Id)
      .Include(m => m.Province);
    return await query.FirstOrDefaultAsync() ?? throw new BadHttpRequestException($"Municipality with id {Id} not found.");
  }
}