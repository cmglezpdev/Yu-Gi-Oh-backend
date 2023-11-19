using AutoMapper;
using backend.Application.Services;
using backend.Domain;
using backend.Domain.Entities;
using backend.Infrastructure.Entities;
using backend.Presentation.DTOs;
using backend.Presentation.Interfaces;
using Microsoft.AspNetCore.Mvc;



namespace backend.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArchetypeController : BaseCrudController<Archetype,ArchetypeDomain>
{
   
    public ArchetypeController(ArchetypeService service) : base(service)
    {
       
    }
    
}