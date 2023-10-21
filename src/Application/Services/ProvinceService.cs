using AutoMapper;
using backend.Application.Interfaces;
using backend.Infraestructure;
using backend.Infraestructure.Entities;
using backend.Presentation.DTOs;
namespace backend.Application.Services;

public class ProvinceService : BaseCrudService<Province, ProvinceCreationDto>
{
  public ProvinceService(AppDbContext context, IMapper mapper) : base(context, mapper) { }
}