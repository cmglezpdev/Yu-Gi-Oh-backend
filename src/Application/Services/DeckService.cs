using AutoMapper;
using backend.Application.Interfaces;
using backend.Application.Repositories;
using backend.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Application.Services;
public class DeckService
{
    private readonly DbContext context;
    private readonly IDeckRepository deckRepository;

    public DeckService(DbContext _context,IMapper _mapper,IDeckRepository deckRepository)
    {
        context = _context;
        this.deckRepository = deckRepository;
    }
    public async Task<IEnumerable<Deck>> GetAllDecksAsync()
    {
        return await deckRepository.GetAllDecksAsync();
    }
    public async Task<Deck> DeleteDeckById(Guid Id)
    {
        return await deckRepository.DeleteDeckByIdAsync(Id);
    }

    public async Task<Deck> GetDeckByIdAsync(Guid Id)
    {
        return await deckRepository.GetDeckByIdAsync(Id);
    }

    public async Task<Deck> CreateDeckAsync(DeckInputDto deck)
    {
        return await deckRepository.CreateDeckAsync(deck);
    }

    public async Task<Deck> UpdateDeckAsync(Guid Id, DeckInputDto dto)
    {
        return await deckRepository.UpdateDeckAsync(Id, dto);
    }
}




