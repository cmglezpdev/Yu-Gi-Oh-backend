using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.Application.Providers;
using backend.Common.Authorization;
using backend.Infrastructure.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace backend.Infrastructure.Authentication;

public sealed class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _options;
    private readonly IPermissionService _permissionService;
    
    public JwtProvider(IOptions<JwtOptions> options, IPermissionService permissionService)
    {
        _permissionService = permissionService;
        _options = options.Value;
    }

    public async Task<string> Generate(User user)
    {
        var claims = new List<Claim>()
        {
            new (JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new (JwtRegisteredClaimNames.Email, user.Email),
            new (JwtRegisteredClaimNames.UniqueName, user.UserName), 
        };
        claims.AddRange(
            user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Name))
        );

        var permissions = await _permissionService.GetPermissionAsync(user.Id); 
        claims.AddRange(
            permissions.Select(permission => new Claim(CustomClaim.Permissions, permission))
        );
        
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            claims,
            null,
            expires: DateTime.Now.AddDays(100),
            signingCredentials);

        var tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return tokenValue;
    }
}