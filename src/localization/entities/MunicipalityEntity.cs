using System.ComponentModel.DataAnnotations.Schema;
using backend.database;
namespace backend.localization;

[Table("municipalities")]
public class Municipality : PlatformModel
{

  [Column(TypeName = "varchar(100)")]
  public string Name { get; set; }

  [ForeignKey("ProvinceId")]
  public Province Province { get; set; }
}