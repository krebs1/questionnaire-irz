using Entities.Models;

namespace Contracts;

public interface IQuestionRepository : IRepositoryBase<Question>
{
    void CreateQuestion(Question question);
    
    void UpdateQuestion(Question question);
    
    void DeleteQuestion(Question question);
    
    Question GetQuestionById(Guid questionId);
    
}