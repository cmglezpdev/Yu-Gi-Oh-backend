using System.ComponentModel.DataAnnotations.Schema;
using backend.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure.Entities;

[Table("Roles")]
[Index(nameof(Name), IsUnique = true)]
public sealed class RoleEntity: PlatformModel
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public List<ClaimsEntity> Claims { get; set; }
    public List<User> Users { get; set; }
    
    public RoleEntity(string name, string? description)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Claims = new List<ClaimsEntity>();
        Users = new List<User>();
    }

    private bool HasClaim(ClaimsEntity claim)
    {
        return Claims.Any(c => c.Id == claim.Id);
    }
    
    public void AddClaim(ClaimsEntity claim)
    {
        if (HasClaim(claim)) return;
        Claims.Add(claim);
    }
    
    public void RemoveClaim(ClaimsEntity claim)
    {
        if (HasClaim(claim))
        {
            Claims.Remove(claim);
        }
    }
    
    public void Update(string? name, string? description)
    {
        Name = name ?? Name;
        Description = description ?? Description;
    }
}