using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using questionnaire.Contracts;
using questionnaire.DTO;

namespace questionnaire.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _config;
    private ITokenService _tokenService;

    public AuthService(UserManager<IdentityUser> userManager, IConfiguration config, ITokenService tokenService)
    {
        _userManager = userManager;
        _config = config;
        _tokenService = tokenService;
    }

    public async Task<IdentityResult> RegisterUser(RegisterDTO user)
    {
        var identityUser = new IdentityUser
        {
            UserName = user.UserName,
            Email = user.UserName,
        };

        return await _userManager.CreateAsync(identityUser, user.Password);
    }

    public async Task<string> Login(IdentityUser user)
    {
        var token = await _tokenService.GenerateToken(user);

        return token;
    }
}