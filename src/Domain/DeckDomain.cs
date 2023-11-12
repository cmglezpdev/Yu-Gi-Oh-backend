
using backend.Domain.Interfaces;
using backend.Infrastructure.Entities;

namespace backend.Domain.Entities{
   public class DeckDomain{
    public Guid Id { get; set; }
    public string Name {get; set;}
    public ArchetypeDomain? Archetype {get; set;}
    public List<ICard>  MainDeck { get; set; }
    public List<ICard> SideDeck { get; set; }
    public List<ICard> ExtraDeck { get; set; }
    public DeckDomain(string name,List<ICard> mainCards,List<ICard> sideCards,List<ICard> extraCards)
    {
        Id = Guid.NewGuid();
        this.Name = name;
        MainDeck = mainCards;
        SideDeck = sideCards;
        ExtraDeck = extraCards;
        Archetype = GetArchetype();


    }
    public ArchetypeDomain? GetArchetype()
    {
       Dictionary<Guid,int> dictionary = new Dictionary<Guid, int>();
       foreach(var item in MainDeck){
            if(item.Archetype == null) continue;
            if(dictionary.ContainsKey(item.Archetype.Id)) 
            {
                dictionary[item.Archetype.Id]++;
            }
             else
            {
                dictionary.Add(item.Archetype.Id, 1);
            }

            if(dictionary[item.Archetype.Id] >= MainDeck.Count() / 2) 
            {
                return item.Archetype;
            }
       }

       return null;
    }
    public void AddCard(ICard card,TypeDecks deck)
    {
        if(deck == TypeDecks.Main)
        {
            MainDeck.Add(card);   
        }
        else if(deck == TypeDecks.Side)
        {
            SideDeck.Add(card);
        }
        else
        {
            ExtraDeck.Add(card);
        }
       Archetype = GetArchetype();
    }
    public void DeleteCard(ICard card)
    {
        for(int i=0;i<MainDeck.Count;i++)
        {
            if(MainDeck[i].Id == card.Id)
            {
                MainDeck.RemoveAt(i);
            }
        }
        for(int i=0;i<SideDeck.Count;i++)
        {
            if(SideDeck[i].Id == card.Id)
            {
                SideDeck.RemoveAt(i);
            }
        }
        for(int i=0;i<ExtraDeck.Count;i++)
        {
            if(ExtraDeck[i].Id == card.Id)
            {
                ExtraDeck.RemoveAt(i);
            }
        }
    }




   }



}