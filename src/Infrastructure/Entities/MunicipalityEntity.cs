using System.ComponentModel.DataAnnotations.Schema;
using backend.Infrastructure.Interfaces;
namespace backend.Infrastructure.Entities;

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
[Table("municipalities")]
public class Municipality : PlatformModel
{

  [Column(TypeName = "varchar(100)")]
  public string Name { get; set; }

  [ForeignKey("ProvinceId")]
  public Province Province { get; set; }
}