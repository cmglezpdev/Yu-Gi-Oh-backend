
using backend.Domain.Interfaces;
using backend.Infrastructure.Entities;

namespace backend.Domain.Entities;
public class DeckDomain
{
    public Guid Id { get; set; }
    public string Name {get; set;}
    public Guid? ArchetypeId {get; set;}
    public int MainDeck { get; set; }
    public int SideDeck { get; set; }
    public int ExtraDeck { get; set; }
    public bool IsActive { get; set; }
    public DeckDomain(string name, int mainCards, int sideCards, int extraCards, Guid? archetype, bool isActive)
    {
        Id = Guid.NewGuid();
        Name = name;
        MainDeck = mainCards;
        SideDeck = sideCards;
        ExtraDeck = extraCards;
        ArchetypeId = archetype;
        IsActive = isActive;
    }
}
