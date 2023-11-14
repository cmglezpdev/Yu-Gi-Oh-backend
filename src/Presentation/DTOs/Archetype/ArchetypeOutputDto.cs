using backend.Infrastructure.Entities;

namespace backend.Presentation.DTOs;

#pragma warning disable CS8618
public class ArchetypeOutputDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}