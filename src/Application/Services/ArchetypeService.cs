using backend.Application.Repositories;
using backend.Infrastructure.Entities;

namespace backend.Application.Services;

public class ArchetypeService
{
    private readonly IArchetypeRepository _archetypeRepository;
    public ArchetypeService(IArchetypeRepository archetypeRepository)
    {
        _archetypeRepository = archetypeRepository;
    }
    public async Task<Archetype> GetArchetypeByIdAsync(Guid Id)
    {
        return await _archetypeRepository.GetArchetypeByIdAsync(Id);
    }
}