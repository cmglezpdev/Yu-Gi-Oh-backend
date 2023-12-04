using backend.Infrastructure.Common;
using backend.Infrastructure.Entities;

namespace backend.Application.Repositories;

public interface IStatRepository: IRepository 
{
    public Task<McResult<IEnumerable<User>>> GetUserWithMoreDecks();
    public Task<McResult<IEnumerable<Archetype>>> GetMostPopularArchetype();
    public Task<McResult<Municipality>> GetMostPopularMunicipalityByArchetype(Guid archetypeId);
    public Task<McResult<IEnumerable<User>>> GetUserWithMostWins();
    public Task<McResult<Archetype>> GetMostPopularArchetypeByTournament(Guid tournamentId);
    public Task<McResult<Municipality>> GetMunicipalityWithMoreWinners();
    public Task<McResult<IEnumerable<Archetype>>> GetMostPopularArchetypeByTournament(Guid tournamentId, int round);
    public Task<McResult<IEnumerable<Archetype>>> GetArchetypeMoreUses(int count);
}