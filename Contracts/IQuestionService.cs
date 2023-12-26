using Entities.Models;
using Microsoft.AspNetCore.Identity;
using questionnaire.DTO;

namespace questionnaire.Contracts;

public interface IQuestionService
{
    Question GetById(Guid id);
    
    Question Create(CreateQuestionDTO createQuestionDto);
    
    Question Update(UpdateQuestionDTO updateQuestionDto);
    
    void Delete(Guid id);
}