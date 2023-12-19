using Contracts;
using Entities;
using Entities.Models;

namespace Repository;

public class QuestionnaireRepository : RepositoryBase<Questionnaire>, IQuestionnaireRepository
{
    public QuestionnaireRepository(RepositoryContext repositoryContext)
        :base(repositoryContext){}
}