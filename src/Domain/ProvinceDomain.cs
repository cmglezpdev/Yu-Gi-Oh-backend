namespace backend.Domain.Entities;

public class ProvinceDomain
{
  public Guid Id { get; set; }
  public string Name { get; set; }
  public List<MunicipalityDomain> Municipalities { get; set; }

  public ProvinceDomain(string name)
  {
    Id = new Guid();
    Name = name;
    Municipalities = new List<MunicipalityDomain>();
  }

  public void AddMuncipality(MunicipalityDomain municipality)
  {
    Municipalities.Add(municipality);
  }
}