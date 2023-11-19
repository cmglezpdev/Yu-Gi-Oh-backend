using AutoMapper;
using backend.Application.Interface;
using backend.Application.Repositories;
using backend.Domain;
using backend.Domain.Entities;
using backend.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Application.Services;

public class ArchetypeService : BaseCrudService<Archetype,ArchetypeDomain>
{
    private readonly IArchetypeRepository _archetypeRepository;
    public ArchetypeService(DbContext context,IMapper mapper):base(context,mapper)
    {
        // _archetypeRepository = archetypeRepository;
    }
    public async Task<Archetype> GetArchetypeByIdAsync(Guid Id)
    {
        return await _archetypeRepository.GetArchetypeByIdAsync(Id);
    }
}