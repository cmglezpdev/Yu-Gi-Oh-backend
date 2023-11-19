using AutoMapper;
using backend.Domain;
using backend.Domain.Entities;
using backend.Infrastructure.Entities;
using backend.Presentation.DTOs;
using backend.Presentation.DTOs.Archetype;
using backend.Presentation.DTOs.Deck;
using backend.Presentation.DTOs.Municipality;
using backend.Presentation.DTOs.Province;
using backend.Presentation.DTOs.User;

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
    
    CreateMap<Archetype, ArchetypeOutputDto>()
      .ForMember(dto => dto.Id, ent => ent.MapFrom(src => src.Id))
      .ForMember(dto => dto.Name, ent => ent.MapFrom(src => src.Name));

    CreateMap<Deck, DeckOutputDto>()
      .ForMember(dto => dto.Id, ent => ent.MapFrom(src => src.Id))
      .ForMember(dto => dto.Name, ent => ent.MapFrom(src => src.Name))
      .ForMember(dto => dto.MainDeck, ent => ent.MapFrom(src => src.MainDeck))
      .ForMember(dto => dto.SideDeck, ent => ent.MapFrom(src => src.SideDeck))
      .ForMember(dto => dto.ExtraDeck, ent => ent.MapFrom(src => src.ExtraDeck))
      .ForMember(dto => dto.ArchetypeId, ent => ent.MapFrom(src => src.ArchetypeId))
      .ForMember(dto => dto.UserId, ent => ent.MapFrom(src => src.UserId));
      
    CreateMap<MunicipalityDomain, Municipality>()
      .ForMember(dto => dto.Id, ent => ent.MapFrom(src => src.Id))
      .ForMember(dto => dto.Name, ent => ent.MapFrom(src => src.Name));
    
    CreateMap<ProvinceDomain, Province>()
      .ForMember(dto => dto.Id, ent => ent.MapFrom(src => src.Id))
      .ForMember(dto => dto.Name, ent => ent.MapFrom(src => src.Name))
      .ForMember(dto => dto.Municipalities, ent => ent.MapFrom(src => src.Municipalities));

    CreateMap<ArchetypeDomain, Archetype>()
      .ForMember(dto => dto.Id, ent => ent.MapFrom(src => src.Id))
      .ForMember(dto => dto.Name, ent => ent.MapFrom(src => src.Name));
      
    CreateMap<DeckDomain, Deck>()
      .ForMember(dto => dto.Id, ent => ent.MapFrom(src => src.Id))
      .ForMember(dto => dto.Name, ent => ent.MapFrom(src => src.Name))
      .ForMember(dto => dto.MainDeck, ent => ent.MapFrom(src => src.MainDeck))
      .ForMember(dto => dto.SideDeck, ent => ent.MapFrom(src => src.SideDeck))
      .ForMember(dto => dto.ExtraDeck, ent => ent.MapFrom(src => src.ExtraDeck))
      .ForMember(dto => dto.ArchetypeId, ent => ent.MapFrom(src => src.ArchetypeId))
      .ForMember(dto => dto.UserId, ent => ent.MapFrom(src => src.UserId));
    
    // CreateMap<User, UserOutputDto>()
    //   .ForMember(dto => dto.Id, ent => ent.MapFrom(src => src.Id))
    //   .ForMember(dto => dto.UserName, ent => ent.MapFrom(src => src.UserName))
    //   .ForMember(dto => dto.Email, ent => ent.MapFrom(src => src.Email))
    //   .ForMember(dto => dto.Name, ent => ent.MapFrom(src => src.Name))
    //   .ForMember(dto => dto.Municipality.Id, ent.MapFrom(src => src.Municipality.Id))
    //   .ForMember(dto => dto.Municipality.Name, ent => ent.MapFrom(src => src.Municipality.Name))
  }
}