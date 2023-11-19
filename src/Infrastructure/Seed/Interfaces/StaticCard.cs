namespace backend.Infrastructure.Seed.Interfaces;

#pragma warning disable CS8618
public class StaticCard
{
    public string name { get; set; }
    public string type { get; set; }
    public string  desc { get; set; }
    public List<StaticImages> card_images { get; set; }
}