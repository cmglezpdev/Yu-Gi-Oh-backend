namespace backend.Domain.Entities;

public class ArchetypeDomain
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<CardDomain> Cards { get; set; }
    public ArchetypeDomain(string name)
    {
        Id = new Guid();
        Name = name;
        Cards = new List<CardDomain>();
    }
    public void AddCards(CardDomain card)
    {
        Cards.Add(card);
    }
}