using System.ComponentModel.DataAnnotations.Schema;
using backend.Infrastructure.Interfaces;

namespace backend.Infrastructure.Entities;

#pragma warning disable CS8618
[Table("archetypes")]
public class Archetype : PlatformModel 
{
    [Column(TypeName = "varchar(100)")]
    public string Name { get; set; }
    public List<Card> Cards { get; set; }
}
