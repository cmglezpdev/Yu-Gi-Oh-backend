using System.ComponentModel.DataAnnotations.Schema;
using backend.Infrastructure.Common;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace backend.Infrastructure.Entities;

[Table("tournaments")]
public class Tournament: PlatformModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Guid MunicipalityId { get; set; }
    public Municipality Municipality { get; set; }
    public Guid? UserId { get; set; }
    public User? User { get; set; }
    public int NumberOfPlayers { get; set; } = 0;
    public int NumberOfInscriptions { get; set; } = 0;
}