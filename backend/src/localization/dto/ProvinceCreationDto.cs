using System.ComponentModel.DataAnnotations;
namespace backend.localization;

public class ProvinceCreationDto
{
  [Required]
  [MaxLength(100)]
  public required string Name { get; set; }

  [Required]
  public required List<MunicipalityCreationDto> Municipalities { get; set; }
}