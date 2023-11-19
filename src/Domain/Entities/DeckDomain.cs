namespace backend.Domain;
public class DeckDomain
{
    public Guid Id { get; set; }
    public string Name {get; set;}
    public Guid? ArchetypeId {get; set;}
    public int MainDeck { get; set; }
    public int SideDeck { get; set; }
    public int ExtraDeck { get; set; }
    public Guid UserId { get; set; }
    public DeckDomain(string name, int mainCards, int sideCards, int extraCards, Guid? archetype, Guid userId)
    {
        Id = Guid.NewGuid();
        Name = name;
        MainDeck = mainCards;
        SideDeck = sideCards;
        ExtraDeck = extraCards;
        ArchetypeId = archetype;
        UserId = userId;
    }
}
