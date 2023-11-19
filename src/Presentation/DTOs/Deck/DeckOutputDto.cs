namespace backend.Presentation.DTOs.Deck;


#pragma warning disable CS8618
public class DeckOutputDto 
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid? ArchetypeId { get; set; }
    public int MainDeck { get; set; }
    public int SideDeck { get; set; }
    public int ExtraDeck { get; set; }
    public Guid UserId { get; set; }
}