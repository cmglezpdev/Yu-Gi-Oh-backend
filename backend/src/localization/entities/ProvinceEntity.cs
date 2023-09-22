using System.ComponentModel.DataAnnotations.Schema;
using backend.database;
namespace backend.localization;

[Table("provinces")]
public class Province : PlatformModel
{
  [Column(TypeName = "varchar(100)")]
  public string Name { get; set; }

  public List<Municipality> Municipalities { get; set; }
}