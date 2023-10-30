using backend.Infrastructure.Entities;

namespace backend.Domain.Entities;

public class MonsterDomain
{
    public Guid Id { get; set; }
    public string Race { get; set; }
    public int? Level { get; set; }
    public int? Atk { get; set; }
    public int? Def { get; set; }

    public MonsterDomain(string race, int? level, int? atk, int? def)
    {
        Id = new Guid();
        Race = race;
        Level = level;
        Atk = atk;
        Def = def;
    }
}