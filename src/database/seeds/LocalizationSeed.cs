using System.IO;
using System.Text.Json;
using backend.localization;
namespace backend.database;


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
      string provinces = await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), "src", "database", "static", "localizations.json"));
      List<Localization> localizations = JsonSerializer.Deserialize<List<Localization>>(provinces)!;

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