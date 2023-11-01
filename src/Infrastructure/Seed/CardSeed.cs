using System.Text.Json;
using AutoMapper;
using backend.Domain.Entities;
using backend.Domain.Interfaces;
using backend.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure.Seed;

public class CardSeed : ISeedCommand
{
    private readonly AppDbContext context;
    private readonly IMapper mapper;
    public CardSeed(AppDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }
    public async Task<bool> Execute()
    {
        var atLeastACard = await context.Cards.FirstOrDefaultAsync();
        if (atLeastACard != null) return true;

        try
        {
            string monsters = await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), "src", "Infrastructure", "Seed", "StaticData", "monsters.json"));
            string spells = await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), "src", "Infrastructure", "Seed", "StaticData", "spells.json"));
            string traps = await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), "src", "Infrastructure", "Seed", "StaticData", "traps.json"));
            string archetypes = await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), "src", "Infrastructure", "Seed", "StaticData", "archetype.json"));

            List<StaticMonster> monsterList = JsonSerializer.Deserialize<List<StaticMonster>>(monsters)!;
            List<StaticCard> spellList = JsonSerializer.Deserialize<List<StaticCard>>(spells)!;
            List<StaticCard> trapList = JsonSerializer.Deserialize<List<StaticCard>>(traps)!;
            Dictionary<string, List<Dictionary<string, string>>> archetypeList = JsonSerializer.Deserialize<Dictionary<string, List<Dictionary<string, string>>>>(archetypes)!;


            var archetypeEntityList = new List<Archetype>();
            var cardsEntityList = new List<Card>();
            var spellCardEntityList = new List<SpellCard>();
            var trapCardEntityList = new List<TrapCard>();
            var monsterCardEntityList = new List<MonsterCard>();

            var CardArchetypeIdDictionary = new Dictionary<string, Guid>();

            foreach (var archetype in archetypeList)
            {
                archetypeEntityList.Add(new Archetype()
                {
                    Id = Guid.NewGuid(),
                    Name = archetype.Key
                });

                foreach (var card in archetype.Value)
                {
                    CardArchetypeIdDictionary.Add(card["name"], archetypeEntityList.Last().Id);
                }
            }

            foreach (var spell in spellList)
            {
                cardsEntityList.Add(new Card()
                {
                    Id = Guid.NewGuid(),
                    Name = spell.name,
                    Type = spell.type,
                    Desc = spell.desc,
                    CardType = "spell",
                    ImageUrl = spell.card_images[0].image_url,
                    ImageUrlSmall = spell.card_images[0].image_url_small,
                    ImageUrlCropped = spell.card_images[0].image_url_cropped,
                    ArchetypeId = CardArchetypeIdDictionary.ContainsKey(spell.name)
                        ? CardArchetypeIdDictionary[spell.name]
                        : null
                });
                spellCardEntityList.Add(new SpellCard()
                {
                    Id = Guid.NewGuid(),
                    CardId = cardsEntityList.Last().Id
                });
            }


            foreach (var trap in trapList)
            {
                cardsEntityList.Add(new Card()
                {
                    Id = Guid.NewGuid(),
                    Name = trap.name,
                    Type = trap.type,
                    Desc = trap.desc,
                    CardType = "trap",
                    ImageUrl = trap.card_images[0].image_url,
                    ImageUrlSmall = trap.card_images[0].image_url_small,
                    ImageUrlCropped = trap.card_images[0].image_url_cropped,
                    ArchetypeId = CardArchetypeIdDictionary.ContainsKey(trap.name)
                        ? CardArchetypeIdDictionary[trap.name]
                        : null
                });
                trapCardEntityList.Add(new TrapCard()
                {
                    Id = Guid.NewGuid(),
                    CardId = cardsEntityList.Last().Id
                });
            }


            foreach (var monster in monsterList)
            {
                cardsEntityList.Add(new Card()
                {
                    Id = Guid.NewGuid(),
                    Name = monster.name,
                    Type = monster.type,
                    Desc = monster.desc,
                    CardType = "monster",
                    ImageUrl = monster.card_images[0].image_url,
                    ImageUrlSmall = monster.card_images[0].image_url_small,
                    ImageUrlCropped = monster.card_images[0].image_url_cropped,
                    ArchetypeId = CardArchetypeIdDictionary.ContainsKey(monster.name)
                        ? CardArchetypeIdDictionary[monster.name]
                        : null
                });
                monsterCardEntityList.Add(new MonsterCard()
                {
                    Id = Guid.NewGuid(),
                    CardId = cardsEntityList.Last().Id,
                    Race = monster.race,
                    Level = monster.level,
                    Atk = monster.atk,
                    Def = monster.def
                });
            }
            
            await context.Archetypes.AddRangeAsync(archetypeEntityList);
            await context.Cards.AddRangeAsync(cardsEntityList);

            await context.SpellCards.AddRangeAsync(spellCardEntityList);
            await context.TrapCards.AddRangeAsync(trapCardEntityList);
            await context.MonsterCards.AddRangeAsync(monsterCardEntityList);

            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception err)
        {
            Console.WriteLine(err);
            return false;
        }
    }
}