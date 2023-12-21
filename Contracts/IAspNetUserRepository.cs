using Microsoft.AspNetCore.Identity;

namespace questionnaire.Contracts;

public interface IAspNetUserRepository
{
    IEnumerable<IdentityUser> GetAllAspNetUsers();
    IdentityUser GetAspNetUserById(Guid aspNetUserId);
    void CreateAspNetUser(IdentityUser identityUser);
    void UpdateAspNetUser(IdentityUser identityUser);
    void DeleteAspNetUser(IdentityUser identityUser);
}