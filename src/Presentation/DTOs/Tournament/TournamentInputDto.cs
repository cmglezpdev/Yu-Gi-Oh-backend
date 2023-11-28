#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace backend.Presentation.DTOs.Tournament;

public class TournamentInputDto
{
    public string Name { get; set; }
    public string Description { get; set; }
 
    public Guid UserId { get; set; }
    public Guid MunicipalityId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}