using Contracts;
using Entities;
using Entities.Models;

namespace Repository;

public class WalkthroughRepository : RepositoryBase<Walkthrough>, IWalkthroughRepository
{
    public WalkthroughRepository(RepositoryContext repositoryContext)
        :base(repositoryContext){}
}