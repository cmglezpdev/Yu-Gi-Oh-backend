using backend.Infrastructure.Seed;
using Microsoft.AspNetCore.Mvc;
namespace backend.Presentation.Controllers;


[ApiController]
[Route("api/seed")]
public class SeedController : ControllerBase
{
  private IEnumerable<ISeedCommand> seedCommands;
  public SeedController(IEnumerable<ISeedCommand> seedCommands)
  {
    this.seedCommands = seedCommands;
  }

  [HttpPost]
  public async Task<ActionResult> ExecuteSeed()
  {
    foreach (var seedCommand in seedCommands)
    {
      await seedCommand.Execute();
    }
    return Ok("Database Seeded!");
  }
}