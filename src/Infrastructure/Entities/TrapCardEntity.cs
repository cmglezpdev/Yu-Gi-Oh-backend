using System.ComponentModel.DataAnnotations.Schema;
using backend.Infrastructure.Interfaces;

namespace backend.Infrastructure.Entities;

#pragma warning disable CS8618

[Table("trap_cards")]
public class TrapCard : PlatformModel
{
  [ForeignKey("CardId")]
  public Card Card { get; set; }
  public Guid CardId { get; set; }
}