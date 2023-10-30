using backend.Infrastructure.Entities;

namespace backend.Domain.Entities;

public class CardDomain
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Desc { get; set; }
    public string ImageUrl { get; set; }
    public string ImageUrlSmall { get; set; }
    public string ImageUrlCropped { get; set; }
    public MonsterDomain? Monster { get; set; }
    public CardDomain(string name, string type, string desc, string imageUrl, string imageUrlSmall, string imageUrlCropped)
    {
        Id = new Guid();
        Name = name;
        Type = type;
        Desc = desc;
        ImageUrl = imageUrl;
        ImageUrlSmall = imageUrlSmall;
        ImageUrlCropped = imageUrlCropped;
    }
    public void AddMonster(string race, int? level, int? atk, int? def)
    {
        Monster = new MonsterDomain(race, level, atk, def);
    }
}