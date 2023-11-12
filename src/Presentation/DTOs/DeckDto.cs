

using backend.Domain.Interfaces;
using backend.Infrastructure.Entities;

namespace backend.Application.Repositories;

public class DeckDto {
   public string Name { get; set; }
   public List<Guid> MainDeck { get; set; }
   public List<Guid> SideDeck { get; set; }
   public List<Guid> ExtraDeck { get; set; }
}