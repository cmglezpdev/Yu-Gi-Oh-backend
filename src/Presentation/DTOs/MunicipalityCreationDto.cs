using System.ComponentModel.DataAnnotations;
using backend.Infraestructure.Entities;
namespace backend.Presentation.DTOs;

public class MunicipalityCreationDto
{
  [Required]
  [MaxLength(100)]
  public required string Name { get; set; }

  [Required]
  public required Province Province { get; set; }
}