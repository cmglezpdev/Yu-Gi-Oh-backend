using backend.Infrastructure.Entities;

namespace backend.Application.Repositories;

public interface ICardRepository : IRepository
{
    Task<Card> GetCardByIdAsync(Guid Id);
    Task<MonsterCard> GetMonsterCardByIdAsync(Guid Id);
}