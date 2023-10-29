using backend.Infrastructure.Entities;

namespace backend.Application.Repositories;

public interface IArchetypeRepository : IRepository
{
    Task<Archetype> GetArchetypeByIdAsync(Guid id);
}