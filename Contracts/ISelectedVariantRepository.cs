using Entities.Models;

namespace Contracts;

public interface ISelectedVariantRepository
{
    public void CreateSelectedVariant(SelectedVariant selectedVariant);
}