using Microsoft.AspNetCore.Identity;
using questionnaire.DTO;

namespace questionnaire.Services;

public interface IAuthService
{
    Task<string> Login(IdentityUser user);
    Task<IdentityResult> RegisterUser(RegisterDTO user);
}