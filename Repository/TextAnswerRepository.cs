using Contracts;
using Entities;
using Entities.Models;

namespace Repository;

public class TextAnswerRepository : RepositoryBase<TextAnswer>, ITextAnswerRepository
{
    public TextAnswerRepository(RepositoryContext repositoryContext)
        :base(repositoryContext){}
}