using AutoMapper;
using backend.Domain.Entities;
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

    CreateMap<MunicipalityDomain, Municipality>()
      .ForMember(dto => dto.Id, ent => ent.MapFrom(src => src.Id))
      .ForMember(dto => dto.Name, ent => ent.MapFrom(src => src.Name));

    CreateMap<ProvinceDomain, Province>()
      .ForMember(dto => dto.Id, ent => ent.MapFrom(src => src.Id))
      .ForMember(dto => dto.Name, ent => ent.MapFrom(src => src.Name))
      .ForMember(dto => dto.Municipalities, ent => ent.MapFrom(src => src.Municipalities));

    // CreateMap<Card, CardOutputDto>()
    //   .ForMember(dto => dto.Id, ent => ent.MapFrom(src => src.Id))
    //   .ForMember(dto => dto.Name, ent => ent.MapFrom(src => src.Name))
    //   .ForMember(dto => dto.Type, ent => ent.MapFrom(src => src.Type))
    //   .ForMember(dto => dto.Desc, ent => ent.MapFrom(src => src.Desc))
    //   .ForMember(dto => dto.ImageUrl, ent => ent.MapFrom(src => src.ImageUrl))
    //   .ForMember(dto => dto.ImageUrlSmall, ent => ent.MapFrom(src => src.ImageUrlSmall))
    //   .ForMember(dto => dto.ImageUrlCropped, ent => ent.MapFrom(src => src.ImageUrlCropped));
    // .ForMember(dto => dto.ArchetypeId, ent => ent.MapFrom(src => src.Archetype.Id))
    // .ForMember(dto => dto.Monster, ent => ent.MapFrom(src => src.Monster)); ;

    // CreateMap<MonsterCard, MonsterCardOutputDto>()
    //   .ForMember(dto => dto.Id, ent => ent.MapFrom(src => src.Id))
    //   .ForMember(dto => dto.Race, ent => ent.MapFrom(src => src.Race))
    //   .ForMember(dto => dto.Level, ent => ent.MapFrom(src => src.Level))
    //   .ForMember(dto => dto.Atk, ent => ent.MapFrom(src => src.Atk))
    //   .ForMember(dto => dto.Def, ent => ent.MapFrom(src => src.Def))
    //   .ForMember(dto => dto.CardId, ent => ent.MapFrom(src => src.Card));

    // CreateMap<Archetype, ArchetypeOutputDto>()
    //   .ForMember(dto => dto.Id, ent => ent.MapFrom(src => src.Id))
    //   .ForMember(dto => dto.Name, ent => ent.MapFrom(src => src.Name));
    //   .ForMember(dto => dto.Cards, ent => ent.MapFrom(src => src.Cards));

    // CreateMap<CardDomain, Card>()
    //   .ForMember(dto => dto.Id, ent => ent.MapFrom(src => src.Id))
    //   .ForMember(dto => dto.Name, ent => ent.MapFrom(src => src.Name))
    //   .ForMember(dto => dto.Type, ent => ent.MapFrom(src => src.Type))
    //   .ForMember(dto => dto.Desc, ent => ent.MapFrom(src => src.Desc))
    //   .ForMember(dto => dto.ImageUrl, ent => ent.MapFrom(src => src.ImageUrl))
    //   .ForMember(dto => dto.ImageUrlSmall, ent => ent.MapFrom(src => src.ImageUrlSmall))
    //   .ForMember(dto => dto.ImageUrlCropped, ent => ent.MapFrom(src => src.ImageUrlCropped));
    // .ForMember(dto => dto.Monster, ent => ent.MapFrom(src => src.Monster));

    // CreateMap<MonsterDomain, MonsterCard>()
    //   .ForMember(dto => dto.Id, ent => ent.MapFrom(src => src.Id))
    //   .ForMember(dto => dto.Race, ent => ent.MapFrom(src => src.Race))
    //   .ForMember(dto => dto.Level, ent => ent.MapFrom(src => src.Level))
    //   .ForMember(dto => dto.Atk, ent => ent.MapFrom(src => src.Atk))
    //   .ForMember(dto => dto.Def, ent => ent.MapFrom(src => src.Def));

    // CreateMap<ArchetypeDomain, Archetype>()
    //   .ForMember(dto => dto.Id, ent => ent.MapFrom(src => src.Id))
    //   .ForMember(dto => dto.Name, ent => ent.MapFrom(src => src.Name));
    // .ForMember(dto => dto.Cards, ent => ent.MapFrom(src => src.Cards));
  }
}

// Mapping Flow
// Se recibe inputDto y se crea un objeto de dominio
// el objeto de dominio se mapea a una entidad
// la entidad se guarda en la base de datos
// se mapea la entidad a un outputDto