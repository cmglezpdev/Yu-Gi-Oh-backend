using AutoMapper;
using backend.Application.Repositories;
using backend.Domain;
using backend.Infrastructure.Entities;
using backend.Presentation.DTOs.Deck;
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

    public async Task<IEnumerable<Deck>> GetAllDecksAsync()
    {
        return await context.Decks.ToListAsync();
    }

    public async Task<Deck> DeleteDeckByIdAsync(Guid Id)
    {
        var deck = await GetDeckByIdAsync(Id);
        context.Decks.Remove(deck);
        await context.SaveChangesAsync();
        return deck;
    }

    public async Task<Deck> GetDeckByIdAsync(Guid Id)
    {
        IQueryable<Deck> query = context.Set<Deck>()
        .Where(m => m.Id == Id);
        return await query.FirstOrDefaultAsync() ?? throw new BadHttpRequestException("No existe ese id") ;
    }

    public async Task<Deck> CreateDeckAsync(DeckInputDto deck)
    {   
        DeckDomain newDeck;
        if(!(deck.ArchetypeId is null))
        {
            Archetype archetype = await archetypeRepository.GetArchetypeByIdAsync((Guid)deck.ArchetypeId);
            newDeck = new DeckDomain(deck.Name, deck.MainDeck, deck.SideDeck, deck.ExtraDeck, archetype.Id, deck.UserId);
        }
        else
        {
            newDeck = new DeckDomain(deck.Name, deck.MainDeck, deck.SideDeck, deck.ExtraDeck, null, deck.UserId);
        }
        
        await context.AddAsync(mapper.Map<Deck>(newDeck));
        await context.SaveChangesAsync();

        return await GetDeckByIdAsync(newDeck.Id);
    }

    public async Task<Deck> UpdateDeckAsync(Guid Id, DeckInputDto dto)
    {
        var deck = await GetDeckByIdAsync(Id);

        deck.Name = dto.Name;
        deck.ArchetypeId = dto.ArchetypeId;
        deck.MainDeck = dto.MainDeck;
        deck.ExtraDeck = dto.ExtraDeck;
        deck.SideDeck = dto.SideDeck;

        context.Update(deck);

        await context.SaveChangesAsync();
        return deck;
    }
}