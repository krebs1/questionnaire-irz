using Entities;
using Microsoft.AspNetCore.Identity;
using questionnaire.Contracts;

namespace Repository;

public class AspNetUserRepository : RepositoryBase<IdentityUser>, IAspNetUserRepository
{
    public AspNetUserRepository(RepositoryContext repositoryContext)
        : base(repositoryContext) {}

    public IEnumerable<IdentityUser> GetAllAspNetUsers()
    {
        return FindAll()
            .ToList();
    }

    public IdentityUser GetAspNetUserById(Guid aspNetUserId)
    {
        return FindByCondition(user => user.Id.Equals(aspNetUserId))
            .FirstOrDefault();
    }

    public void CreateAspNetUser(IdentityUser identityUser)
    {
        Create(identityUser);
    }

    public void UpdateAspNetUser(IdentityUser identityUser)
    {
        Update(identityUser);
    }

    public void DeleteAspNetUser(IdentityUser identityUser)
    {
        Delete(identityUser);
    }
}