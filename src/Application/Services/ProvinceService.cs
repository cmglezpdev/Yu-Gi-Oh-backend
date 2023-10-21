using AutoMapper;
using backend.Application.Interfaces;
using backend.Infrastructure;
using backend.Infrastructure.Entities;
using backend.Presentation.DTOs;
namespace backend.Application.Services;

public class ProvinceService : BaseCrudService<Province, ProvinceCreationDto>
{
  public ProvinceService(AppDbContext context, IMapper mapper) : base(context, mapper) { }
}