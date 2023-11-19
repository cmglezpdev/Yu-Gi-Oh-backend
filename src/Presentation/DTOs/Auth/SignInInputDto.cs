#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace backend.Presentation.DTOs.Auth;

public record SignInInputDto
{
    public string? Email { get; set; }
    
    public string? UserName { get; set; }
    public string Password { get; set; }
}