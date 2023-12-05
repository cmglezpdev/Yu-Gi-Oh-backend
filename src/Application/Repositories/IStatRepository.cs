using backend.Infrastructure.Common;
using backend.Infrastructure.Entities;

namespace backend.Application.Repositories;

public interface IStatRepository: IRepository 
{
    public Task<McResult<IEnumerable<User>>> GetUserWithMoreDecks(int take);
    public Task<McResult<IEnumerable<Archetype>>> GetMostPopularArchetype(int take);
    public Task<McResult<Municipality>> GetMostPopularMunicipalityByArchetype(Guid archetypeId);
    public Task<McResult<IEnumerable<User>>> GetUserWithMostWins(DateTime startDate, DateTime endDate, int take);
    public Task<McResult<Archetype>> GetMostPopularArchetypeByTournament(Guid tournamentId);
    public Task<McResult<Municipality>> GetMunicipalityWithMoreWinners(DateTime startDate, DateTime endDate);
    public Task<McResult<Archetype>> GetMostPopularArchetypeByTournamentAndRound(Guid tournamentId, int round);
    public Task<McResult<IEnumerable<Archetype>>> GetArchetypeMoreUses(int take);
    public Task<McResult<IEnumerable<dynamic>>> GetManyArchetypeWinner();
}