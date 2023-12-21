using Contracts;
using Entities;
using Entities.Models;

namespace Repository;

public class QuestionRepository : RepositoryBase<Question>, IQuestionRepository
{
    public QuestionRepository(RepositoryContext repositoryContext)
        :base(repositoryContext){}
    
    public void CreateQuestion(Question question)
    {
        Create(question);
    }
    public void UpdateQuestion(Question question)
    {
        Update(question);
    }
    public void DeleteQuestion(Question question)
    {
        Delete(question);
    }
    public Question GetQuestionById(Guid questionId)
    {
        return FindByCondition(question => question.QuestionId.Equals(questionId))
            .FirstOrDefault();
    }
}