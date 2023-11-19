namespace backend.Domain.Entities;

public class ArchetypeDomain
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ArchetypeDomain(string name)
    {
        Id = new Guid();
        Name = name;
    }
}