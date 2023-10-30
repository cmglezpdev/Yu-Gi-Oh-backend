using backend.Application.Repositories;
using backend.Infrastructure.Entities;

namespace backend.Application.Services;

public class CardService
{
    private readonly ICardRepository _cardRepository;
    public CardService(ICardRepository cardRepository)
    {
        _cardRepository = cardRepository;
    }
    public async Task<Card> GetCardByIdAsync(Guid Id)
    {
        return await _cardRepository.GetCardByIdAsync(Id);
    }
    public async Task<MonsterCard> GetMonsterCardByIdAsync(Guid Id)
    {
        return await _cardRepository.GetMonsterCardByIdAsync(Id);
    }
}