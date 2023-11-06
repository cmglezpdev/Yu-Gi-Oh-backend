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
     
      public DeckService(DbContext _context,IMapper _mapper): base (_context,_mapper)
      {

      }
      
        
      

    }
}



