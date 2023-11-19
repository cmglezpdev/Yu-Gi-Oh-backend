using backend.Presentation.DTOs.Municipality;

namespace backend.Presentation.DTOs.Province;

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
public class ProvinceOutputDto
{
  public Guid Id { get; set; }
  public string Name { get; set; }
  public List<MunicipalityOutputDto> Municipalities { get; set; }
}