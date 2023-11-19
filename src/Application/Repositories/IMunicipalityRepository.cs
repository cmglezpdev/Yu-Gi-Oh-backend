using backend.Infrastructure.Entities;
namespace backend.Application.Repositories;

public interface IMunicipalityRepository : IRepository
{
  Task<Municipality?> GetMunicipalityByIdAsync(Guid id);
}