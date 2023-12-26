using Microsoft.AspNetCore.Identity;

namespace questionnaire.Contracts;

public interface ITokenService
{
    public Task<string> GenerateToken(IdentityUser user);
}