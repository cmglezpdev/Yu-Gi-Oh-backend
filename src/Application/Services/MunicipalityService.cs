using AutoMapper;
using backend.Application.Interfaces;
using backend.Infrastructure;
using backend.Infrastructure.Entities;
using backend.Presentation.DTOs;
namespace backend.Application.Services;

public class MunicipalityService : BaseCrudService<Municipality, MunicipalityCreationDto>
{
  public MunicipalityService(AppDbContext context, IMapper mapper) : base(context, mapper) { }
}