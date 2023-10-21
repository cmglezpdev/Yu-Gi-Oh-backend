using backend.Application.Repositories;
using backend.Infrastructure.Entities;

namespace backend.Application.Services;

public class ProvinceService
{
  private readonly IProvinceRepository _provinceRepository;
  public ProvinceService(IProvinceRepository provinceRepository)
  {
    _provinceRepository = provinceRepository;
  }

  public async Task<IEnumerable<Province>> GetProvincesAsync()
  {
    return await _provinceRepository.GetProvincesAsync();
  }

  public async Task<Province> GetProvinceByIdAsync(Guid Id)
  {
    return await _provinceRepository.GetProvinceByIdAsync(Id);
  }
}