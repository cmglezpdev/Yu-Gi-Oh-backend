#pragma warning disable CS8618
namespace backend.Presentation.DTOs.Auth;

public record SignUpInputDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string UserName { get; set; }
    public Guid MunicipalityId { get; set; }
}