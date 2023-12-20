using Microsoft.AspNetCore.Identity;
using questionnaire.DTO;

namespace questionnaire.Services;

public interface IAuthService
{
    string GenerateTokenString(GenTokenDTO user);
    Task<bool> Login(LoginDTO user);
    Task<bool> RegisterUser(RegisterDTO user);
}