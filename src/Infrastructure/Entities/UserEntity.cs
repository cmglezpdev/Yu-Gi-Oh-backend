using System.ComponentModel.DataAnnotations.Schema;
using backend.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
namespace backend.Infrastructure.Entities;

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
[Table("users")]
[Index(nameof(Email), IsUnique = true)]
[Index(nameof(UserName), IsUnique = true)]
public class User : PlatformModel
{
  [Column(TypeName = "varchar(100)")]
  public required string Name { get; set; }

  [Column(TypeName = "varchar(100)")]
  public required string Email { get; set; }

  [Column(TypeName = "varchar(100)")]
  public string UserName { get; set; }
  
  [Column(TypeName = "varchar(200)")]
  public required string Password { get; set; }

  [ForeignKey("MunicipalityId")]
  public Municipality Municipality { get; set; }
  public required Guid MunicipalityId { get; set; }
  public List<RoleEntity> Roles { get; set; }
}