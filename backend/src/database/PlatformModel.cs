using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace backend.database;

public abstract class PlatformModel
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public Guid Id { get; set; }

  [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
  [DefaultValue("NOW()")]
  public DateTime CreatedAt { get; set; }

  [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
  [DefaultValue("NOW()")]
  public DateTime UpdatedAt { get; set; }
}