using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace questionnaire.Contracts;

public interface IAspNetRoleRepository
{
    IEnumerable<IdentityRole> GetAllAspNetRoles();
    IdentityRole GetAspNetRoleById(Guid aspNetRoleId);
    IdentityRole GetAspNetRoleByName(string aspNetRoleName);
    void CreateAspNetRole(IdentityRole identityRole);
    void UpdateAspNetRole(IdentityRole identityRole);
    void DeleteAspNetRole(IdentityRole identityRole);
}