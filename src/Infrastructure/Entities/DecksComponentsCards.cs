using System.ComponentModel.DataAnnotations.Schema;
using backend.Domain.Interfaces;
using backend.Infrastructure.Common;

namespace backend.Infrastructure.Entities;

#pragma warning disable CS8618
[Table("decks_cards")]
public class DecksComponentCards : PlatformModel
{    
    [ForeignKey("CardId")]
    public Guid CardId {get;set;}

    [ForeignKey("DeckId")]
    public Guid DeckId {get; set;}

    [Column]
    public TypeDecks DeckType { get; set; }
}