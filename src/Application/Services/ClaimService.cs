using backend.Infrastructure;
using backend.Infrastructure.Common;
using backend.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Application.Services;

public class ClaimService
{
    private readonly AppDbContext _context;

    public ClaimService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<McResult<List<ClaimsEntity>>> GetAllClaimsAsync()
    {
        var claims = await _context.Claims.ToListAsync();
        return McResult<List<ClaimsEntity>>.Succeed(claims);
    }
    
    public async Task<McResult<ClaimsEntity>> GetClaimByIdAsync(Guid id)
    {
        var claim = await _context.Claims
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();
        
        return claim is null 
            ? McResult<ClaimsEntity>.Failure("Claim not found") 
            : McResult<ClaimsEntity>.Succeed(claim);
    }
}