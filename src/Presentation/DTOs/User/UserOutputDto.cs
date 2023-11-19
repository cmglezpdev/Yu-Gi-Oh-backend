using backend.Presentation.DTOs.Municipality;

#pragma warning disable CS8618
namespace backend.Presentation.DTOs.User;

public class UserOutputDto
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public MunicipalityOutputDto Municipality { get; set; }
    public string Token { get; set; }
    
}