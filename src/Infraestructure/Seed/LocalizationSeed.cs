using System.Text.Json;
using backend.Infrastructure.Entities;
namespace backend.Infrastructure.Seed;


public class LocalizationSeed : ISeedCommand
{
  private readonly AppDbContext context;

  public LocalizationSeed(AppDbContext context)
  {
    this.context = context;
  }


  public async Task<bool> Execute()
  {
    try
    {
      string provinces = await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), "src", "Infrastructure", "Seed", "StaticData", "localizations.json"));
      List<StaticLocalization> localizations = JsonSerializer.Deserialize<List<StaticLocalization>>(provinces)!;

      foreach (var localization in localizations)
      {
        Province province = new Province()
        {
          Name = localization.nombre,
          Municipalities = new List<Municipality>()
        };

        foreach (var municipality in localization.municipios)
        {
          province.Municipalities.Add(new Municipality() { Name = municipality });
        }

        context.Provinces.Add(province);
        await context.SaveChangesAsync();
      }
      return true;
    }
    catch (Exception err)
    {
      Console.WriteLine(err);
      return false;
    }
  }
}