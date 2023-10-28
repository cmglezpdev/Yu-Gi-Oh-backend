namespace backend.Presentation.DTOs;

#pragma warning disable CS8618
public class CardOutputDto
{
    public Guid id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Desc  { get; set; }
    public Guid ArchetypeId { get; set; }
}