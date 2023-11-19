using System.ComponentModel.DataAnnotations.Schema;
using backend.Infrastructure.Common;

namespace backend.Infrastructure.Entities;

#pragma warning disable CS8618
[Table("archetypes")]
public class Archetype : PlatformModel
{
    [Column(TypeName = "varchar(100)")]
    public string Name { get; set; }
}
