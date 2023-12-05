using backend.Application.Services;
using backend.Common.Authorization;
using backend.Infrastructure.Authentication;
using backend.Presentation.DTOs.Inscriptions;
using backend.Presentation.DTOs.Tournament;
using Microsoft.AspNetCore.Mvc;

namespace backend.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InscriptionController : ControllerBase
{
    private readonly InscriptionService _service;

    public InscriptionController(InscriptionService service)
    {
        _service = service;
    }
    
    [HttpGet]
    [HasPermission(Permission.WriteInscription)]
    public async Task<ActionResult> FindAllInscriptions([FromQuery]InscriptionFilterDto filter)
    {
        var response = await _service.FindAllInscriptions(filter);
        if(response.IsFailure) return BadRequest(response);
        return Ok(response);
    }
    
    [HttpGet("{id:Guid}")]
    [HasPermission(Permission.ReadInscription)]
    public async Task<ActionResult> FindInscriptionById(Guid id)
    {
        var response = await _service.FindInscriptionById(id);
        if(response.IsFailure) return BadRequest(response);
        return Ok(response);
    }
    
    [HttpPost]
    [HasPermission(Permission.WriteInscription)]
    public async Task<ActionResult> CreateInscription([FromBody] InscribeDto input)
    {
        var response = await _service.CreateInscription(input);
        if(response.IsFailure) return BadRequest(response);
        return Ok(response);
    }
    
    [HttpPatch("reject")]
    [HasPermission(Permission.RejectOrAcceptInscription)]
    public async Task<ActionResult> RejectInscription([FromBody] ApproveOrRejectInscription input)
    {
        var response = await _service.RejectInscription(input.InscriptionId);
        if(response.IsFailure) return BadRequest(response);
        return Ok(response);
    }
    
    [HttpPatch("approve")]
    [HasPermission(Permission.RejectOrAcceptInscription)]
    public async Task<ActionResult> ApproveInscription([FromBody] ApproveOrRejectInscription input)
    {
        var response = await _service.AcceptInscription(input.InscriptionId);
        if(response.IsFailure) return BadRequest(response);
        return Ok(response);
    }

}

