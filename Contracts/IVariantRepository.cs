using Entities.Models;

namespace Contracts;

public interface IVariantRepository : IRepositoryBase<Variant>
{
    void CreateVariant(Variant variant);
    void UpdateVariant(Variant variant);
    void DeleteVariant(Variant variant);
    Variant GetVariantById(Guid variantId);
}