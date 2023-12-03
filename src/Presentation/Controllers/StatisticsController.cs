using backend.Application.Services;
using backend.Presentation.DTOs.Duels;
using Microsoft.AspNetCore.Mvc;

namespace backend.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatisticsController : ControllerBase
{
    private readonly DuelsService _service;

    public StatisticsController(DuelsService service)
    {
        _service = service;
    }
}