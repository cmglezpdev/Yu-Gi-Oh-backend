

using backend.Infrastructure.Entities;

namespace backend.Application.Repositories{

    public interface IDeckRepository:IRepository{
      Task<Deck> GetDeckById(Guid Id);
      Task<Deck> PostDeck(DeckDto deck);
      Task<Deck> PutDeckById(Guid Id);
      Task<Deck> DeleteDeckById(Guid Id);

    }
}