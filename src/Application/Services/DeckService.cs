using AutoMapper;
using backend.Application.Interfaces;
using backend.Application.Repositories;
using backend.Domain.Interfaces;
using backend.Infrastructure;
using backend.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;


namespace backend.Application.Services
{
    public class DeckService : BaseCrudService<Deck,DeckDto>{
        private readonly DbContext context;
        private readonly IDeckRepository deckRepository;

        public DeckService(DbContext _context,IMapper _mapper,IDeckRepository deckRepository): base (_context,_mapper)
       {
            context = _context;
            this.deckRepository = deckRepository;
        }
       public async Task<Deck> DeleteDeckById(Guid Id)
    {
        return await deckRepository.DeleteDeckById(Id);
    }

    public async Task<Deck> GetDeckById(Guid Id)
    {
       return await deckRepository.GetDeckById(Id);
    }

    public async Task<Deck> PostDeck(DeckDto deck)
    {
        return await deckRepository.PostDeck(deck);
    }

    public async Task<Deck> PutDeckById(Guid Id)
    {
        return await deckRepository.PutDeckById(Id);
    }
        
      

    }
}



