using backend.Application.Repositories;
using backend.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure.Repositories;

public class ArchetypeRepository : IArchetypeRepository
{
    private readonly AppDbContext _context;
    public ArchetypeRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Archetype> GetArchetypeByIdAsync(Guid Id)
    {
        IQueryable<Archetype> query = _context.Set<Archetype>()
            .Where(m => m.Id == Id);
        return await query.FirstOrDefaultAsync() ?? throw new BadHttpRequestException($"Card with id {Id} not found.");
    }
}