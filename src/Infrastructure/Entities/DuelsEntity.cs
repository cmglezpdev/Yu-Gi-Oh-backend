using System.ComponentModel.DataAnnotations.Schema;
using backend.Common.Enums;
using backend.Infrastructure.Common;

namespace backend.Infrastructure.Entities;

[Table("duels")]
public class DuelsEntity : PlatformModel
{
    public Guid PlayerAId { get; set; }
    public Guid PlayerBId { get; set; }
    public int PlayerAScore { get; set; } = 0;
    public int PlayerBScore { get; set; } = 0;
    public Guid? PlayerWinner { get; set; }
    public Guid TournamentId { get; set; }
    public int Round { get; set; }
    public int DuelNumber { get; set; } = 0;
    
    public DuelsEntity() {}
    
    public DuelsEntity(Guid tournamentId, Guid playerAId, Guid playerBId, int round = 0)
    {
        TournamentId = tournamentId;
        PlayerAId = playerAId;
        PlayerBId = playerBId;
        Round = round;
    }

    public McResult<string> RealizeDuel(char winner)
    {
        if(winner != 'A' && winner != 'B')
            return McResult<string>.Failure("Invalid winner. Must be A or B", ErrorCodes.InvalidInput);

        DuelNumber++;
        if(winner == 'A') PlayerAScore++;
        else PlayerBScore++;

        if (DuelNumber == 3)
        {
            PlayerWinner = PlayerAScore > PlayerBScore ? PlayerAId : PlayerBId;
        }
        
        return McResult<string>.Succeed("");
    }
}