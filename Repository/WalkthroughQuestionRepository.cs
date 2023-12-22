using Contracts;
using Entities;
using Entities.Models;

namespace Repository;

public class WalkthroughQuestionRepository: RepositoryBase<WalkthroughQuestion>, IWalkthroughQuestionRepository
{
    public WalkthroughQuestionRepository(RepositoryContext repositoryContext)
        :base(repositoryContext){}

    public void CreateWalkthroughQuestion(WalkthroughQuestion walkthroughQuestion)
    {
        Create(walkthroughQuestion);
    }
}