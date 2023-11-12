using System.ComponentModel.DataAnnotations.Schema;
using backend.Infrastructure.Interfaces;

namespace backend.Infrastructure.Entities;

#pragma warning disable CS8618
[Table("decks")]
public class Deck : PlatformModel
{
    [Column(TypeName = "varchar(100)")]
    public string Name { get; set; }
    
    [ForeignKey("ArchetypeId")]
    public Archetype? Archetype { get; set; }

    public Guid? ArchetypeId { get; set; }
}