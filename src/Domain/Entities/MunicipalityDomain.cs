namespace backend.Domain;

public class MunicipalityDomain
{
  public Guid Id { get; set; }
  public string Name { get; set; }

  public MunicipalityDomain(string name, Guid? id)
  {
    Id = id ?? Guid.NewGuid();
    Name = name;
  }
}