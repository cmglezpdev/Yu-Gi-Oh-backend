using backend.Application.Repositories;
using backend.Infrastructure.Entities;

namespace backend.Application.Services;

public class CardService
{
    private readonly ICardRepository _cardepository;
    public CardService(ICardRepository cardRepository)
    {
        _cardepository = cardRepository;
    }
    public async Task<Card> GetCardByIdAsync(Guid Id)
    {
        return await _cardepository.GetCardByIsAsync(Id);
    }
}