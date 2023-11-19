using System.Text.Json;
using AutoMapper;
using backend.Domain;
using backend.Infrastructure.Entities;
using backend.Infrastructure.Seed.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace backend.Infrastructure.Seed;


public class LocalizationSeed : ISeedCommand
{
  private readonly AppDbContext _context;
  private readonly IMapper _mapper;

  public LocalizationSeed(AppDbContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }


  public async Task<bool> Execute()
  {
    var atLeastAProvince = await _context.Provinces.FirstOrDefaultAsync();
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
          province.AddMuncipality(new MunicipalityDomain(name, null));
        }

        await _context.Provinces.AddAsync(_mapper.Map<Province>(province));
        await _context.SaveChangesAsync();
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