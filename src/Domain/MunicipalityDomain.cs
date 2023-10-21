namespace backend.Domain.Entities;

public class MunicipalityDomain
{
  public Guid Id { get; set; }
  public string Name { get; set; }

  public MunicipalityDomain(string name)
  {
    Id = new Guid();
    Name = name;
  }
}