using backend.Infrastructure.Entities;

namespace backend.Application.Repositories;

public interface ICardRepository : IRepository
{
    Task<Card> GetCardByIsAsync(Guid Id);
}