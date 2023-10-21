using AutoMapper;
using backend.database;
namespace backend.localization;

public class MunicipalityService : BaseCrudService<Municipality, MunicipalityCreationDto>
{
  public MunicipalityService(AppDbContext context, IMapper mapper) : base(context, mapper) { }
}