namespace backend.Application.Repositories;

#pragma warning disable CS8618
public class DeckInputDto {
   public string Name { get; set; }
   public Guid? ArchetypeId { get; set; }
   public int MainDeck { get; set; }
   public int SideDeck { get; set; }
   public int ExtraDeck { get; set; }
}