using AutoMapper;
using backend.Application.Repositories;
using backend.Domain;
using backend.Infrastructure.Entities;
using backend.Presentation.DTOs.Deck;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure.Repositories;

public class DeckRepository : IDeckRepository
{
    private readonly AppDbContext _context;

    private readonly IArchetypeRepository _archetypeRepository;

    private readonly IMapper _mapper;

    public DeckRepository(AppDbContext context, IArchetypeRepository archetypeRepository, IMapper mapper)
    {
        _context = context;
        _archetypeRepository = archetypeRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Deck>> GetDecksByUserAsync(Guid Id)
    {
        IQueryable<Deck> query = _context.Set<Deck>()
            .Where(d => d.UserId == Id);
        return await query.ToListAsync() ?? throw new BadHttpRequestException("No existe ese id") ;
    }

    public async Task<Deck> DeleteDeckByIdAsync(Guid Id)
    {
        var deck = await GetDeckByIdAsync(Id);
        _context.Decks.Remove(deck);
        await _context.SaveChangesAsync();
        return deck;
    }

    public async Task<Deck> GetDeckByIdAsync(Guid Id)
    {
        IQueryable<Deck> query = _context.Set<Deck>()
        .Where(m => m.Id == Id);
        return await query.FirstOrDefaultAsync() ?? throw new BadHttpRequestException("No existe ese id") ;
    }

    public async Task<Deck> CreateDeckAsync(DeckInputDto deck)
    {   
        DeckDomain newDeck;
        if(!(deck.ArchetypeId is null))
        {
            Archetype archetype = await _archetypeRepository.GetArchetypeByIdAsync((Guid)deck.ArchetypeId);
            newDeck = new DeckDomain(deck.Name, deck.MainDeck, deck.SideDeck, deck.ExtraDeck, archetype.Id, deck.UserId);
        }
        else
        {
            newDeck = new DeckDomain(deck.Name, deck.MainDeck, deck.SideDeck, deck.ExtraDeck, null, deck.UserId);
        }
        
        await _context.AddAsync(_mapper.Map<Deck>(newDeck));
        await _context.SaveChangesAsync();

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

        _context.Update(deck);

        await _context.SaveChangesAsync();
        return deck;
    }
}