using AutoMapper;
using backend.Application.Repositories;
using backend.Infrastructure;
using backend.Infrastructure.Common;
using backend.Infrastructure.Entities;
using backend.Presentation.DTOs.Deck;
using Microsoft.EntityFrameworkCore;

namespace backend.Application.Services;
public class StatService
{
    private readonly AppDbContext _context;
    private readonly IStatRepository statRepository;

    public StatService(AppDbContext context,IMapper _mapper,IStatRepository statRepository)
    {
        _context = context;
        this.statRepository = statRepository;
    }

    public async Task<McResult<IEnumerable<User>>> GetUserWithMoreDecks()
    {
        return await statRepository.GetUserWithMoreDecks();
    }
    public async Task<McResult<IEnumerable<Archetype>>> GetMostPopularArchetype()
    {
        return await statRepository.GetMostPopularArchetype();
    }
    public async Task<McResult<Municipality>> GetMostPopularMunicipalityByArchetype(Guid archetypeId)
    {
        return await statRepository.GetMostPopularMunicipalityByArchetype(archetypeId);
    }
    public async Task<McResult<IEnumerable<User>>> GetUserWithMostWins()
    {
        return await statRepository.GetUserWithMostWins();
    }
    public async Task<McResult<Archetype>> GetMostPopularArchetypeByTournament(Guid tournamentId)
    {
        return await statRepository.GetMostPopularArchetypeByTournament(tournamentId);
    }
    public async Task<McResult<Municipality>> GetMunicipalityWithMoreWinners()
    {
        return await statRepository.GetMunicipalityWithMoreWinners();
    }
    public async Task<McResult<IEnumerable<Archetype>>> GetMostPopularArchetypeByTournament(Guid tournamentId, int round)
    {
        return await statRepository.GetMostPopularArchetypeByTournament(tournamentId, round);
    }
    public async Task<McResult<IEnumerable<Archetype>>> GetArchetypeMoreUses(int count)
    {
        return await statRepository.GetArchetypeMoreUses(count);
    }
}