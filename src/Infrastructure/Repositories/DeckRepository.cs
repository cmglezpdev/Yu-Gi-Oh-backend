



using System.Xml.Linq;
using backend.Application.Repositories;
using backend.Domain.Entities;
using backend.Domain.Interfaces;
using backend.Infrastructure.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure.Repositories;

public class DeckRepository : IDeckRepository
{
    private readonly AppDbContext context;
    private readonly ICardRepository cardRepository;

    public DeckRepository(AppDbContext context, ICardRepository cardRepository)
    {
        this.context = context;
        this.cardRepository = cardRepository;
    }

    public async Task<Deck> DeleteDeckById(Guid Id)
    {
        var deck = await GetDeckById(Id);
        deck.IsActive = false;
        context.Decks.Update(deck);
        await context.SaveChangesAsync();
        return deck;
    }

    public async Task<Deck> GetDeckById(Guid Id)
    {
        IQueryable<Deck> query = context.Set<Deck>()
        .Where(m => m.Id == Id && m.IsActive);
        return await query.FirstOrDefaultAsync() ?? throw new BadHttpRequestException("No existe ese id") ;
    }

    public async Task<Deck> PostDeck(DeckDto deck)
    {
        // var mainDeck = new List<ICard>();
        // foreach(var cardId in deck.MainDeck) {
        //    mainDeck.Add( await cardRepository.GetCardByIdAsync(cardId));
        // }

        throw new NotImplementedException();

        // var x = await Task.WaitAll(tasks.ToArray());
        // var x = new DeckDomain(deck.Name, )
    //    context.Decks.AddAsync(nw);
    }

    public Task<Deck> PutDeckById(Guid Id)
    {
        throw new NotImplementedException();
    }
}