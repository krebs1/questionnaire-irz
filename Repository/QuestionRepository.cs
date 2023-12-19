using Contracts;
using Entities;
using Entities.Models;

namespace Repository;

public class QuestionRepository : RepositoryBase<Question>, IQuestionRepository
{
    public QuestionRepository(RepositoryContext repositoryContext)
        :base(repositoryContext){}
}