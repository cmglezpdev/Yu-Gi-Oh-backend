namespace backend.Presentation.DTOs;

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
public class ProvinceOutputDto
{
  public Guid Id { get; set; }
  public string Name { get; set; }
  public List<MunicipalityOutputDto> Municipalities { get; set; }
}