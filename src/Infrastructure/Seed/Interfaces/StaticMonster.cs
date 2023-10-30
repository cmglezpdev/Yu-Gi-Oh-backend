namespace backend.Infrastructure.Seed;

#pragma warning disable CS8618
public class StaticMonster
{
    public string name { get; set; }
    public string type { get; set; }
    public string  desc { get; set; }
    public int? atk { get; set; }
    public int? def { get; set; }
    public int? level { get; set; }
    public  string race { get; set; }
    public List<StaticImages> card_images { get; set; }
}
public class StaticImages
{
    public int id { get; set; }
    public string image_url { get; set; }
    public string image_url_small { get; set; }
    public string image_url_cropped { get; set; }
}