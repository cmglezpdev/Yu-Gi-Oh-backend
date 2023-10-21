using Microsoft.AspNetCore.Mvc;
namespace backend.Presentation.Controllers;

[ApiController]
[Route("/api/users")]
public class UsersController : ControllerBase
{
  private readonly ILogger<UsersController> logger;

  public UsersController(
    ILogger<UsersController> logger
  )
  {
    this.logger = logger;
  }
}