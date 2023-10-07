
using System.ComponentModel.DataAnnotations;
using backend.localization;

public class MunicipalityCreationDto
{
  [Required]
  [MaxLength(100)]
  public required string Name { get; set; }

  [Required]
  public required Province Province { get; set; }
}