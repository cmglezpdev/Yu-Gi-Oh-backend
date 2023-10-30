using backend.Application.Repositories;
using backend.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure.Repositories;

public class CardRepository : ICardRepository
{
    private readonly AppDbContext _context;
    public CardRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Card> GetCardByIdAsync(Guid Id)
    {
        IQueryable<Card> query = _context.Set<Card>()
            .Where(m => m.Id == Id);
        return await query.FirstOrDefaultAsync() ?? throw new BadHttpRequestException($"Card with id {Id} not found.");
    }
    public async Task<MonsterCard> GetMonsterCardByIdAsync(Guid Id)
    {
        IQueryable<MonsterCard> query = _context.Set<MonsterCard>()
            .Where(m => m.Id == Id);
        return await query.FirstOrDefaultAsync() ?? throw new BadHttpRequestException($"Monster Card with id {Id} not found.");
    }
}