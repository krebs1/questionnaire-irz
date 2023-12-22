using Entities.Models;

namespace Contracts;

public interface IWalkthroughRepository
{
    void CreateWalkthrough(Walkthrough walkthrough);
    void UpdateWalkthrough(Walkthrough walkthrough);
    void DeleteWalkthrough(Walkthrough walkthrough);
    Walkthrough GetWalkthroughById(Guid walkthroughId);
    IEnumerable<Walkthrough> GetAllWalkthroughs();
}