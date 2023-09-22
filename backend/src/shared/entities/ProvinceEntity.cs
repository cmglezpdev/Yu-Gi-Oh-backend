
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("municipalities")]
public class Province
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public Guid Id { get; set; }

  [Column(TypeName = "varchar(100)")]
  public string Name { get; set; }

  public List<Municipality> Municipalities { get; set; }
}