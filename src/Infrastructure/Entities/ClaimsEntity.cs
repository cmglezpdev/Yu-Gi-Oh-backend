using System.ComponentModel.DataAnnotations.Schema;
using backend.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure.Entities;

[Table("Claims")]
[Index(nameof(Name), IsUnique = true)]
public class ClaimsEntity: PlatformModel
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public List<RoleEntity> Roles { get; set; }
    
    public ClaimsEntity(string name, string? description)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Roles = new List<RoleEntity>();
    }
    
    public void Update(string? name, string? description)
    {
        Name = name ?? Name;
        Description = description ?? Description;
    }
}