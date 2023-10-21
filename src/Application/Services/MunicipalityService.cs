using backend.Application.Repositories;
using backend.Infrastructure.Entities;

namespace backend.Application.Services;

public class MunicipalityService
{
  private readonly IMunicipalityRepository _municipalityRepository;
  public MunicipalityService(IMunicipalityRepository municipalityRepository)
  {
    _municipalityRepository = municipalityRepository;
  }

  public async Task<Municipality> GetMunicipalityByIdAsync(Guid Id)
  {
    return await _municipalityRepository.GetMunicipalityByIdAsync(Id);
  }
}