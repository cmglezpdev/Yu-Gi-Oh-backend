using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Infrastructure.Entities;

#pragma warning disable CS8618
public class MonsterCard : Card 
{
    [Column(TypeName = "varchar(100)")]
    public string Rice { get; set; }

    [Column]
    public int Level { get; set; }

    [Column]
    public int Atk { get; set; }

    [Column]
    public int Def { get; set; } 
}