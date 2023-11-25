using backend.Infrastructure.Common.Enums;

namespace backend.Presentation.DTOs.Inscriptions;

public class InscriptionFilterDto
{   
    public Guid? TournamentId { get; set; }
    public InscriptionStatus? Status { get; set; }
}