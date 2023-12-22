using Entities.Models;

namespace Contracts;

public interface IWalkthroughQuestionRepository
{
    void CreateWalkthroughQuestion(WalkthroughQuestion walkthroughQuestion);
}