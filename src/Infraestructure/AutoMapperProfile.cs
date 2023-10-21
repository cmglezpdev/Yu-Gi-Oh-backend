using AutoMapper;
using backend.Infraestructure.Entities;
using backend.Presentation.DTOs;
namespace backend.Infraestructure;
public class AutoMapperProfiles : Profile
{
  public AutoMapperProfiles()
  {
    CreateMap<MunicipalityCreationDto, Municipality>();
    CreateMap<ProvinceCreationDto, Province>();
  }
}