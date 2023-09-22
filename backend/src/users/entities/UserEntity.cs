using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.users;

[Table("users")]
public class User
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public required Guid Id { get; set; }

  [Column(TypeName = "varchar(100)")]
  public required string Name { get; set; }

  [Column(TypeName = "varchar(100)")]
  public required string Email { get; set; }

  [Column(TypeName = "varchar(200)")]
  public required string Password { get; set; }

  [ForeignKey("MunicipalityId")]
  public required Municipality Municipality { get; set; }
  // municipality, role, deckId
}