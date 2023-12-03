using backend.Application.Services;
using backend.Common.Authorization;
using backend.Infrastructure.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace backend.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClaimController: ControllerBase
{
    private readonly ClaimService _service;

    public ClaimController(ClaimService service)
    {
        _service = service;
    }

    [HttpGet]
    [HasPermission(Permission.ReadPermission)]
    public async Task<ActionResult> GetAllClaimsAsync()
    {
        var claimsResponse = await _service.GetAllClaimsAsync();
        return Ok(claimsResponse);
    }
    
    [HttpGet("{id:Guid}")]
    [HasPermission(Permission.ReadPermission)]
    public async Task<ActionResult> GetClaimByIdAsync(Guid id)
    {
        var claimResponse = await _service.GetClaimByIdAsync(id);
        return claimResponse.IsFailure
            ? NotFound(claimResponse)
            : Ok(claimResponse);
    }
}