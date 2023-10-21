using AutoMapper;
using backend.Infrastructure.Entities;
using backend.Presentation.DTOs;
namespace backend.Infrastructure;
public class AutoMapperProfiles : Profile
{
  public AutoMapperProfiles()
  {
    CreateMap<MunicipalityCreationDto, Municipality>();
    CreateMap<ProvinceCreationDto, Province>();
  }
}