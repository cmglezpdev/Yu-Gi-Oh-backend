using System.Text.Json;
using AutoMapper;
using backend.Domain.Entities;
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
        if(atLeastACard != null) return true;

        try
        {
            string monsters = await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), "src", "Infrastructure", "Seed", "StaticData", "monsters.json"));
            string spells = await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), "src", "Infrastructure", "Seed", "StaticData", "spells.json"));
            string traps = await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), "src", "Infrastructure", "Seed", "StaticData", "traps.json"));
            string archetypes = await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), "src", "Infrastructure", "Seed", "StaticData", "archetype.json"));
           
            List<StaticMonster> monsterList = JsonSerializer.Deserialize<List<StaticMonster>>(monsters)!;   
            List<StaticCard> spellList = JsonSerializer.Deserialize<List<StaticCard>>(spells)!;
            List<StaticCard> trapList = JsonSerializer.Deserialize<List<StaticCard>>(traps)!;
            Dictionary<string,List<Dictionary<string,string>>> archetypeList = JsonSerializer.Deserialize<Dictionary<string,List<Dictionary<string,string>>>>(archetypes)!;

            foreach(var arche in archetypeList.Keys)
            {   
                ArchetypeDomain archetype = new ArchetypeDomain(arche);
                if(!(archetypeList[arche].Count == 0))
                {   
                    foreach(var archetypeCard in archetypeList[arche])
                    {  
                        //Busco la carta tanto en las magias como las trampas sino esta, busco en los monstruos
                        var cardWithArchetype = spellList.Find(card => card.name == archetypeCard["name"]);
                        if(cardWithArchetype is default(StaticCard))
                        {
                            cardWithArchetype = trapList.Find( card => card.name == archetypeCard["name"]);
                        }
                        if(cardWithArchetype is default(StaticCard))
                        {
                            var monster = monsterList.Find(card => card.name == archetypeCard["name"]);
                            if(!(monster is default(StaticMonster)))
                            {
                                CardDomain card = new CardDomain(monster.name, monster.type, monster.desc, monster.card_images[0].image_url, monster.card_images[0].image_url_small, monster.card_images[0].image_url_cropped);
                                archetype.AddCards(card);
                                //MonsterDomain monsterCard = new MonsterDomain(monster.race, monster.level, monster.atk, monster.def, card);
                                //context.MonsterCards.Add(mapper.Map<MonsterCard>(monsterCard));
                                await context.SaveChangesAsync();
                                monsterList.Remove(monster);
                            }
                        }
                        else
                        {
                            CardDomain card = new CardDomain(cardWithArchetype.name, cardWithArchetype.type, cardWithArchetype.desc, cardWithArchetype.card_images[0].image_url, cardWithArchetype.card_images[0].image_url_small, cardWithArchetype.card_images[0].image_url_cropped);
                            archetype.AddCards(card);
                            //elimino en los dos pq no se donde esta
                            spellList.Remove(cardWithArchetype);
                            trapList.Remove(cardWithArchetype);
                        }
                    }
                }
                context.Archetypes.Add(mapper.Map<Archetype>(archetype));
                await context.SaveChangesAsync();
            }
            foreach(var monster in monsterList)
            {
                CardDomain card = new CardDomain(monster.name, monster.type, monster.desc, monster.card_images[0].image_url, monster.card_images[0].image_url_small, monster.card_images[0].image_url_cropped);
                context.Cards.Add(mapper.Map<Card>(card));
                //MonsterDomain monsterCard = new MonsterDomain(monster.race, monster.level, monster.atk, monster.def, card);
                //context.MonsterCards.Add(mapper.Map<MonsterCard>(monsterCard));
                await context.SaveChangesAsync();
            }
            foreach(var spell in spellList)
            {
                CardDomain card = new CardDomain(spell.name, spell.type, spell.desc, spell.card_images[0].image_url, spell.card_images[0].image_url_small, spell.card_images[0].image_url_cropped);
                context.Cards.Add(mapper.Map<Card>(card));
                await context.SaveChangesAsync();
            }
            foreach(var trap in trapList)
            {
                CardDomain card = new CardDomain(trap.name, trap.type, trap.desc, trap.card_images[0].image_url, trap.card_images[0].image_url_small, trap.card_images[0].image_url_cropped);
                context.Cards.Add(mapper.Map<Card>(card));
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