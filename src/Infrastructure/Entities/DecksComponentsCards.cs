using System.ComponentModel.DataAnnotations.Schema;
using backend.Infrastructure.Interfaces;

namespace backend.Infrastructure.Entities;

#pragma warning disable CS8618
[Table("decks_components_cards")]
public class DecksComponentCards : PlatformModel
{
    [ForeignKey("MainDeckId")]
    public Guid MainDeckId {get;set;}
    [ForeignKey("CardId")]
    public Guid CardId {get;set;}
}