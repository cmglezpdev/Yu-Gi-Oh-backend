using AutoMapper;
using backend.localization;
namespace backend.database;

public class AutoMapperProfiles : Profile
{
  public AutoMapperProfiles()
  {
    CreateMap<MunicipalityCreationDto, Municipality>();
    CreateMap<ProvinceCreationDto, Province>();
  }
}