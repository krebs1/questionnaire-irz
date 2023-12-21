using Contracts;
using Entities;
using Entities.Models;

namespace Repository;

public class VariantRepository : RepositoryBase<Variant>, IVariantRepository
{
    public VariantRepository(RepositoryContext repositoryContext)
        :base(repositoryContext){}
    
    public void CreateVariant(Variant variant)
    {
        Create(variant);
    }
    public void UpdateVariant(Variant variant)
    {
        Update(variant);
    }
    public void DeleteVariant(Variant variant)
    {
        Delete(variant);
    }
    public Variant GetVariantById(Guid variantId)
    {
        return FindByCondition(variant => variant.VariantId.Equals(variantId))
            .FirstOrDefault();
    }
}