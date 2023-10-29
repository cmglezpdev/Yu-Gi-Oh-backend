using backend.Application.Repositories;
using backend.Infrastructure.Entities;

namespace backend.Application.Services;

public class CardService
{
    private readonly ICardRepository _cardRepository;
    private readonly IMonsterCardRepository _monsterCardRepository;
    public CardService(ICardRepository cardRepository, IMonsterCardRepository monsterCardRepository)
    {
        _cardRepository = cardRepository;
        _monsterCardRepository = monsterCardRepository;
    }
    public async Task<Card> GetCardByIdAsync(Guid Id)
    {
        return await _cardRepository.GetCardByIsAsync(Id);
    }
    public async Task<MonsterCard> GetMonsterCardByIdAsync(Guid Id)
    {
        return await _monsterCardRepository.GetMonsterCardByIdAsync(Id);
    }
}