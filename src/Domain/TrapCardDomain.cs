using backend.Domain.Interfaces;

namespace backend.Domain.Entities;

public class TrapCardDomain : ICard
{
  public Guid Id { get; set; }
  public string Name { get; set; }
  public TypeCards TypeCard { get; set; }
  public string Type { get; set; }
  public string Desc { get; set; }
  public string ImageUrl { get; set; }
  public string ImageUrlSmall { get; set; }
  public string ImageUrlCropped { get; set; }
  public ArchetypeDomain? Archetype { get; set; }

  public TrapCardDomain(string name, string type, string desc, string imageUrl, string imageUrlSmall, string imageUrlCropped, ArchetypeDomain? archetype)
  {
    Id = new Guid();
    Name = name;
    Type = type;
    Desc = desc;
    TypeCard = TypeCards.Trap;
    ImageUrl = imageUrl;
    ImageUrlSmall = imageUrlSmall;
    ImageUrlCropped = imageUrlCropped;
    Archetype = archetype;
  }
}
