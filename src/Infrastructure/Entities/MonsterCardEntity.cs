using System.ComponentModel.DataAnnotations.Schema;
using backend.Infrastructure.Common;

namespace backend.Infrastructure.Entities;

#pragma warning disable CS8618
[Table("monster_cards")]
public class MonsterCard : PlatformModel
{
    [Column(TypeName = "varchar(100)")]
    public string Race { get; set; }

    [Column]
    public int? Level { get; set; }

    [Column]
    public int? Atk { get; set; }

    [Column]
    public int? Def { get; set; }

    [ForeignKey("CardId")]
    public Card Card { get; set; }

    public Guid CardId { get; set; }
}