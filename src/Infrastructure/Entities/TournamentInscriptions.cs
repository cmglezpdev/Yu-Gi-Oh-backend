using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using backend.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure.Entities;

[Table("tournament_inscriptions")]
public class TournamentInscriptions : PlatformModel
{
    public Guid TournamentId { get; set; }
    public Guid UserId { get; set; }
    public Guid DeckId { get; set; }
    public bool IsApproved { get; set; } = false;
}