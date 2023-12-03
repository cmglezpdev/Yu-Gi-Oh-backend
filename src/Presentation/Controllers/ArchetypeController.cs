using backend.Application.Services;
using backend.Common.Authorization;
using backend.Domain.Entities;
using backend.Infrastructure.Authentication;
using backend.Infrastructure.Entities;
using backend.Presentation.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[HasPermission(Permission.ReadArchetype)]
public class ArchetypeController : BaseCrudController<Archetype,ArchetypeDomain>
{
    public ArchetypeController(ArchetypeService service) : base(service)
    {
    }
}