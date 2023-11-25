using backend.Application.Services;
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
    public async Task<ActionResult> FindAllInscriptions()
    {
        var response = await _service.FindAllInscriptions();
        if(response.IsFailure) return BadRequest(response);
        return Ok(response);
    }
    
    [HttpGet("{id:Guid}")]
    public async Task<ActionResult> FindInscriptionById(Guid id)
    {
        var response = await _service.FindInscriptionById(id);
        if(response.IsFailure) return BadRequest(response);
        return Ok(response);
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateInscription([FromBody] InscribeDto input)
    {
        var response = await _service.CreateInscription(input);
        if(response.IsFailure) return BadRequest(response);
        return Ok(response);
    }
    
    [HttpPatch("reject")]
    public async Task<ActionResult> RejectInscription([FromBody] ApproveOrRejectInscription input)
    {
        var response = await _service.RejectInscription(input.InscriptionId);
        if(response.IsFailure) return BadRequest(response);
        return Ok(response);
    }
    
    [HttpPatch("approve")]
    public async Task<ActionResult> ApproveInscription([FromBody] ApproveOrRejectInscription input)
    {
        var response = await _service.AcceptInscription(input.InscriptionId);
        if(response.IsFailure) return BadRequest(response);
        return Ok(response);
    }

}









