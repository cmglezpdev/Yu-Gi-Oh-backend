using System.ComponentModel.DataAnnotations.Schema;
using backend.Infrastructure.Interfaces;

namespace backend.Infrastructure.Entities;

#pragma warning disable CS8618

[Table("spell_cards")]
public class SpellCard : PlatformModel
{
  [ForeignKey("CardId")]
  public Card Card { get; set; }
  public Guid CardId { get; set; }
}