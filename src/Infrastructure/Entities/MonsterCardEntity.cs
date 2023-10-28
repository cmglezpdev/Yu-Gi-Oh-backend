using System.ComponentModel.DataAnnotations.Schema;
using backend.Infrastructure.Interfaces;

namespace backend.Infrastructure.Entities;

#pragma warning disable CS8618
[Table("monsters")]
public class MonsterCard : PlatformModel
{
    [Column(TypeName = "varchar(100)")]
    public string Rice { get; set; }

    [Column]
    public int Level { get; set; }

    [Column]
    public int Atk { get; set; }

    [Column]
    public int Def { get; set; } 

    [ForeignKey("CardId")]
    public Card Card { get; set; }

}