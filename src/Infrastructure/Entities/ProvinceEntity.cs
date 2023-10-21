using System.ComponentModel.DataAnnotations.Schema;
using backend.Infrastructure.Interfaces;
namespace backend.Infrastructure.Entities;

[Table("provinces")]
public class Province : PlatformModel
{
  [Column(TypeName = "varchar(100)")]
  public string Name { get; set; }

  public List<Municipality> Municipalities { get; set; }
}