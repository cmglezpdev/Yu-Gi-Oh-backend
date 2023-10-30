namespace backend.Presentation.DTOs;

#pragma warning disable CS8618
public class CardOutputDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Desc  { get; set; }
    public string ImageUrl { get; set; }
    public string ImageUrlSmall { get; set; }
    public string ImageUrlCropped { get; set; }
    public Guid ArchetypeId { get; set; }
}