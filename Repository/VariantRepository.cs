using Contracts;
using Entities;
using Entities.Models;

namespace Repository;

public class VariantRepository : RepositoryBase<Variant>, IVariantRepository
{
    public VariantRepository(RepositoryContext repositoryContext)
        :base(repositoryContext){}
}