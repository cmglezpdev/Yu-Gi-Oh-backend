using System.ComponentModel.DataAnnotations.Schema;
using backend.Infrastructure.Interfaces;

namespace backend.Infrastructure.Entities;

#pragma warning disable CS8618
[Table("decks_components")]
public class DecksComponent : PlatformModel
{
    
    [ForeignKey("MainDeckId")]
    public Guid MainDeckId {get;set;}
    
    [Column(TypeName ="varchar(100)")]
    public string Type {get; set;}
    
}