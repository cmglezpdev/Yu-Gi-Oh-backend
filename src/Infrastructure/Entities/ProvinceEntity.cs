using System.ComponentModel.DataAnnotations.Schema;
using backend.Infrastructure.Common;

namespace backend.Infrastructure.Entities;

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
[Table("provinces")]
public class Province : PlatformModel
{
  [Column(TypeName = "varchar(100)")]
  public string Name { get; set; }

  public List<Municipality> Municipalities { get; set; }
}