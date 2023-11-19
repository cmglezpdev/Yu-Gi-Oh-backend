using backend.Domain.Entities;
using backend.Domain.Interfaces;

namespace backend.Domain;


public class MonsterCardDomain : ICard
{
  public Guid Id { get; set; }
  public string Name { get; set; }
  public TypeCards TypeCard { get; set; }
  public string Type { get; set; }
  public string Desc { get; set; }
  public string ImageUrl { get; set; }
  public string ImageUrlSmall { get; set; }
  public string ImageUrlCropped { get; set; }
  public string Race { get; set; }
  public int? Level { get; set; }
  public int? Atk { get; set; }
  public int? Def { get; set; }
  public ArchetypeDomain? Archetype { get; set; }

  public MonsterCardDomain(
    string name, string type, string desc,
    string imageUrl, string imageUrlSmall,
    string imageUrlCropped, string race,
    int? level, int? atk, int? def, ArchetypeDomain? archetype
  )
  {
    Id = new Guid();
    Name = name;
    Type = type;
    Desc = desc;
    TypeCard = TypeCards.Monster;
    ImageUrl = imageUrl;
    ImageUrlSmall = imageUrlSmall;
    ImageUrlCropped = imageUrlCropped;
    Race = race;
    Level = level;
    Atk = atk;
    Def = def;
    Archetype = archetype;
  }
}
