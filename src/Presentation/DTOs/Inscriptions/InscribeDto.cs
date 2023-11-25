namespace backend.Presentation.DTOs.Tournament;

public class InscribeDto
{
    public Guid UserId { get; set; }
    public Guid DeckId { get; set; }
    public Guid TournamentId { get; set; }
}