using System.Text.Json;
using AutoMapper;
using backend.Domain.Entities;
using backend.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
namespace backend.Infrastructure.Seed;


public class LocalizationSeed : ISeedCommand
{
  private readonly AppDbContext context;
  private readonly IMapper mapper;

  public LocalizationSeed(AppDbContext context, IMapper mapper)
  {
    this.context = context;
    this.mapper = mapper;
  }


  public async Task<bool> Execute()
  {
    var atLeastAProvince = await context.Provinces.FirstOrDefaultAsync();
    if (atLeastAProvince != null) return true;

    try
    {
      string provinces = await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), "src", "Infrastructure", "Seed", "StaticData", "localizations.json"));
      List<StaticLocalization> localizations = JsonSerializer.Deserialize<List<StaticLocalization>>(provinces)!;

      foreach (var localization in localizations)
      {
        ProvinceDomain province = new ProvinceDomain(localization.nombre);
        foreach (var name in localization.municipios)
        {
          province.AddMuncipality(new MunicipalityDomain(name));
        }

        await context.Provinces.AddAsync(mapper.Map<Province>(province));
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