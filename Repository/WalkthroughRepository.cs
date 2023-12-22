using Contracts;
using Entities;
using Entities.Models;

namespace Repository;

public class WalkthroughRepository : RepositoryBase<Walkthrough>, IWalkthroughRepository
{
    public WalkthroughRepository(RepositoryContext repositoryContext)
        :base(repositoryContext){}

    public void CreateWalkthrough(Walkthrough walkthrough)
    {
        Create(walkthrough);
    }

    public void UpdateWalkthrough(Walkthrough walkthrough)
    {
        Update(walkthrough);
    }

    public void DeleteWalkthrough(Walkthrough walkthrough)
    {
        Delete(walkthrough);
    }

    public Walkthrough GetWalkthroughById(Guid walkthroughId)
    {
        return FindByCondition(walkthrough => walkthrough.WalkthroughId.Equals(walkthroughId))
            .FirstOrDefault();
    }

    public IEnumerable<Walkthrough> GetAllWalkthroughs()
    {
        return FindAll()
            .ToList();
    }
}