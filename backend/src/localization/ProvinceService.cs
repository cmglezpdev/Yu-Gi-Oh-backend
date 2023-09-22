using AutoMapper;
using backend.localization;
namespace backend.database;

public class ProvinceService : BaseCrudService<Province, ProvinceCreationDto>
{
  public ProvinceService(AppDbContext context, IMapper mapper) : base(context, mapper) { }
}