using AutoMapper;
using backend.database;
namespace backend.localization;

public class ProvinceService : BaseCrudService<Province, ProvinceCreationDto>
{
  public ProvinceService(AppDbContext context, IMapper mapper) : base(context, mapper) { }
}