using AutoMapper;
using backend.Application.Repositories;
using backend.Domain.Entities;
using backend.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure.Repositories;

public class DeckRepository : IDeckRepository
{
    private readonly AppDbContext context;

    private readonly IArchetypeRepository archetypeRepository;

    private readonly IMapper mapper;

    public DeckRepository(AppDbContext context, IArchetypeRepository archetypeRepository, IMapper mapper)
    {
        this.context = context;
        this.archetypeRepository = archetypeRepository;
        this.mapper = mapper;
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

    public async Task<Deck> PostDeck(DeckInputDto deck)
    {   
        DeckDomain newDeck;
        if(!(deck.ArchetypeId is null))
        {
            Archetype archetype = await archetypeRepository.GetArchetypeByIdAsync((Guid)deck.ArchetypeId);
            newDeck = new DeckDomain(deck.Name, deck.MainDeck, deck.SideDeck, deck.ExtraDeck, archetype.Id, true);
        }
        else
        {
            newDeck = new DeckDomain(deck.Name, deck.MainDeck, deck.SideDeck, deck.ExtraDeck, null, true);
        }
        
        await context.AddAsync(mapper.Map<Deck>(newDeck));
        await context.SaveChangesAsync();

        return await GetDeckById(newDeck.Id);
    }

    public async Task<Deck> PutDeckById(Guid Id)
    {
        return await GetDeckById(Id);
    }
}