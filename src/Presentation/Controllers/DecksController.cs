using backend.Presentation.Interfaces;
using backend.Infrastructure.Entities;
using backend.Application.Interfaces;
using backend.Application.Services;
using backend.Application.Repositories;
// using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace backend.Application.Controllers
{
[ApiController]    
[Route("api/[controller]")]
 public class DecksController : BaseCrudController<Deck,DeckDto>{
 
   public DecksController(DeckService deckService) : base(deckService)
   {
    
   }
   


}


}