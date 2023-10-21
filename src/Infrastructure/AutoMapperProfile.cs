using AutoMapper;
using backend.Infrastructure.Entities;
using backend.Presentation.DTOs;
namespace backend.Infrastructure;
public class AutoMapperProfiles : Profile
{
  public AutoMapperProfiles()
  {
    CreateMap<Municipality, MunicipalityOutputDto>()
      .ForMember(dto => dto.Id, ent => ent.MapFrom(src => src.Id))
      .ForMember(dto => dto.Name, ent => ent.MapFrom(src => src.Name))
      .ForMember(dto => dto.ProvinceId, ent => ent.MapFrom(src => src.Province.Id));

    CreateMap<Province, ProvinceOutputDto>()
      .ForMember(dto => dto.Id, ent => ent.MapFrom(src => src.Id))
      .ForMember(dto => dto.Name, ent => ent.MapFrom(src => src.Name))
      .ForMember(dto => dto.Municipalities, ent => ent.MapFrom(src => src.Municipalities));
  }
}