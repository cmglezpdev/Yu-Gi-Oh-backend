using System.ComponentModel.DataAnnotations.Schema;
using backend.Infraestructure.Interfaces;
namespace backend.Infraestructure.Entities;

[Table("provinces")]
public class Province : PlatformModel
{
  [Column(TypeName = "varchar(100)")]
  public string Name { get; set; }

  public List<Municipality> Municipalities { get; set; }
}