using System.Text.Json;
using backend.Domain;
using backend.Infrastructure.Entities;
using backend.Infrastructure.Seed.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure.Seed;

public class RolesAndClaimsSeed: ISeedCommand
{
    private readonly AppDbContext _context;
    
    public RolesAndClaimsSeed(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> Execute()
    {
        var someRole = await _context.Roles.FirstOrDefaultAsync();
        var someClaim = await _context.Claims.FirstOrDefaultAsync();
        if (someRole is not null || someClaim is not null) return false;
        
        try
        {
            // save claims
            var claimsFile = await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), "src", "Infrastructure", "Seed", "StaticData", "claims.json"));
            var claimsName = JsonSerializer.Deserialize<List<string>>(claimsFile)!;
            var claims = claimsName.Select(name => new ClaimsEntity(name, null)).ToList();
            
            await _context.Claims.AddRangeAsync(claims);
            await _context.SaveChangesAsync();
            
            
            // save roles
            var rolesFile = await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), "src", "Infrastructure", "Seed", "StaticData", "roles.json"));
            var staticRoles = JsonSerializer.Deserialize<List<StaticRole>>(rolesFile)!;
            var roles = new List<RoleEntity>();

            foreach (var staticRole in staticRoles)
            {
                // get all claims that are in the static role
                var roleClaims = claims.Where(claim => staticRole.Claims.Contains(claim.Name)).ToList();
                var role = new RoleEntity(staticRole.Name, null);
                roleClaims.ForEach(claim => role.AddClaim(claim));
                
                roles.Add(role);
            }
            
            await _context.Roles.AddRangeAsync(roles);
            await _context.SaveChangesAsync();
            
            return true;
        }
        catch (Exception err)
        {
            Console.WriteLine(err);
            return false;
        }
    }
}