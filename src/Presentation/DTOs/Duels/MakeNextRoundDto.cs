namespace backend.Presentation.DTOs.Duels;

public class MakeNextRoundDto
{
    public Guid TournamentId { get; set; }
    public int CurrentRound { get; set; }
    public int AmountOfPlayers { get; set; }
}