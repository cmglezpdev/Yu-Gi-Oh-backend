using backend.Infrastructure.Entities;

namespace backend.Application.Repositories;

public interface IMonsterCardRepository : IRepository
{
    Task<MonsterCard> GetMonsterCardByIdAsync(Guid id);
}