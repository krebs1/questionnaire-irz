using Contracts;
using Entities;
using Entities.Models;

namespace Repository;

public class QuestionRepository : RepositoryBase<Question>, IQuestionRepository
{
    public QuestionRepository(RepositoryContext repositoryContext)
        :base(repositoryContext){}
    
    public async void CreateQuestion(Question question)
    {
      Create(question);
    }
    public async void UpdateQuestion(Question question)
    {
        Update(question);
    }
    public async void DeleteQuestion(Question question)
    {
        Delete(question);
    }
    public Question GetQuestionById(Guid questionId)
    {
        return FindByCondition(question => question.QuestionId.Equals(questionId))
            .FirstOrDefault();
    }
}