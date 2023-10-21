using backend.Infrastructure.Entities;
namespace backend.Application.Repositories;

public interface IProvinceRepository : IRepository
{
  Task<Province> GetProvinceByIdAsync(Guid Id);
  Task<IEnumerable<Province>> GetProvincesAsync();
}