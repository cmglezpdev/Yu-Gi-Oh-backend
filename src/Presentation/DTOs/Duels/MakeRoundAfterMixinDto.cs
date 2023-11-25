namespace backend.Presentation.DTOs.Duels;

public class MakeRoundAfterMixinDto
{
    public Guid TournamentId { get; set; }
    public int AmountOfPlayers { get; set; }
}