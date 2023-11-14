using backend.Infrastructure.Entities;

namespace backend.Application.Repositories{

    public interface IDeckRepository:IRepository{
      Task<IEnumerable<Deck>> GetAllDecksAsync();
      Task<Deck> GetDeckByIdAsync(Guid Id);
      Task<Deck> CreateDeckAsync(DeckInputDto deck);
      Task<Deck> UpdateDeckAsync(Guid Id, DeckInputDto deck);
      Task<Deck> DeleteDeckByIdAsync(Guid Id);

    }
}