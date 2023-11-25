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

        if(PlayerWinner is not null)
            return McResult<string>.Failure("The duel has already been played", ErrorCodes.OperationError);
        
        DuelNumber++;
        if(winner == 'A') PlayerAScore++;
        else PlayerBScore++;

        if (DuelNumber == 3 || (DuelNumber == 2 && PlayerAScore != PlayerBScore))
        {
            PlayerWinner = PlayerAScore > PlayerBScore ? PlayerAId : PlayerBId;
        }
        
        return McResult<string>.Succeed("");
    }
}