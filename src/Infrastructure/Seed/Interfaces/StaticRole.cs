#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace backend.Infrastructure.Seed.Interfaces;

public sealed class StaticRole
{
    public string Name { get; set; }
    public List<string> Claims { get; set; }
}