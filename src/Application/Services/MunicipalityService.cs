using AutoMapper;
using backend.Application.Interfaces;
using backend.Infraestructure;
using backend.Infraestructure.Entities;
using backend.Presentation.DTOs;
namespace backend.Application.Services;

public class MunicipalityService : BaseCrudService<Municipality, MunicipalityCreationDto>
{
  public MunicipalityService(AppDbContext context, IMapper mapper) : base(context, mapper) { }
}