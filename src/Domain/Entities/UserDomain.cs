using backend.Infrastructure.Entities;

#pragma warning disable CS8618
namespace backend.Domain;

public class UserDomain
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public MunicipalityDomain Municipality { get; set; }
    
    public List<RoleEntity> Roles { get; set; }
    
    public UserDomain(string email, string userName, string password, string name, MunicipalityDomain municipality)
    {
        Id = Guid.NewGuid();
        Email = email;
        UserName = userName;
        Password = BCrypt.Net.BCrypt.HashPassword(password);
        Name = name;
        Municipality = municipality;
        Roles = new List<RoleEntity>();
    }

    private bool HasRole(RoleEntity role)
    {
        return Roles.Any(r => r.Id == role.Id);
    }
    public void AddRole(RoleEntity role)
    {
        if (HasRole(role)) return;
        Roles.Add(role);   
    }
    
    public void RemoveRole(RoleEntity role)
    {
        if (HasRole(role))
        {
            Roles.Remove(role);
        }
    }
}