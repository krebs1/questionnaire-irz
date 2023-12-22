using Contracts;
using Entities;
using Entities.Models;

namespace Repository;

public class SelectedVariantRepository : RepositoryBase<SelectedVariant>, ISelectedVariantRepository
{
    public SelectedVariantRepository(RepositoryContext repositoryContext)
        :base(repositoryContext){}

    public void CreateSelectedVariant(SelectedVariant selectedVariant)
    {
        Create(selectedVariant);
    }
}