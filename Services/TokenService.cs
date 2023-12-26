using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using questionnaire.Contracts;

namespace questionnaire.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IClaimService _claimService;

    public TokenService(IConfiguration config, UserManager<IdentityUser> userManager, IClaimService claimService)
    {
        _config = config;
        _userManager = userManager;
        _claimService = claimService;
    }

    public async Task<string> GenerateToken(IdentityUser user)
    {
        var tokenDescriptor = await GenerateTokenDescriptor(user);

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(securityToken);
    }

    private async Task<ClaimsIdentity> GenerateClaims(IdentityUser user)
    {
        var userRoles = await _userManager.GetRolesAsync(user);
        var identityOptions = new IdentityOptions();
        var claims = new ClaimsIdentity(new []
        {
            new Claim("userId", user.Id),
            new Claim("userName", user.UserName)
        });
        claims.AddClaims(_claimService.GenClaims(identityOptions.ClaimsIdentity.RoleClaimType, userRoles));

        return claims;
    }

    private async Task<SecurityTokenDescriptor> GenerateTokenDescriptor(IdentityUser user)
    {
        var claims = await GenerateClaims(user);
        
        return new SecurityTokenDescriptor
        {
            Subject = claims,
            Expires = DateTime.UtcNow.AddSeconds(Convert.ToDouble(_config.GetSection("Jwt:ExpiresSeconds").Value)),
            Issuer = _config.GetSection("Jwt:Issuer").Value,
            Audience = _config.GetSection("Jwt:Audience").Value,
            SigningCredentials = new SigningCredentials
            (
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value)), 
                SecurityAlgorithms.HmacSha512Signature
            )
        };
    }
}