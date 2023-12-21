using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using questionnaire.Contracts;

namespace Repository;

public class AspNetRoleRepository : RepositoryBase<IdentityRole>, IAspNetRoleRepository
{
    public AspNetRoleRepository(RepositoryContext repositoryContext)
        :base(repositoryContext){}
    
    public IEnumerable<IdentityRole> GetAllAspNetRoles()
    {
        return FindAll()
            .ToList();
    }

    public IdentityRole GetAspNetRoleById(Guid aspNetRoleId)
    {
        return FindByCondition(role => role.Id.Equals(aspNetRoleId.ToString()))
            .FirstOrDefault();
    }

    public IdentityRole GetAspNetRoleByName(string aspNetRoleName)
    {
        return FindByCondition(role => role.Name.Equals(aspNetRoleName))
            .FirstOrDefault();
    }

    public void CreateAspNetRole(IdentityRole identityRole)
    {
        Create(identityRole);
    }

    public void UpdateAspNetRole(IdentityRole identityRole)
    {
        Update(identityRole);
    }

    public void DeleteAspNetRole(IdentityRole identityRole)
    {
        Delete(identityRole);
    }
}