using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using questionnaire.Contracts;
using questionnaire.DTO;

namespace questionnaire.Services;

public class QuestionService : IQuestionService
{
    private IRepositoryWrapper _repository;
    
    private IMapper _mapper;
    
    public QuestionService (IRepositoryWrapper repository, IMapper mapper) 
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public Question Create(CreateQuestionDTO createQuestionDto)
    {
        var questionEntity = _mapper.Map<Question>(createQuestionDto);
            
        _repository.Question.CreateQuestion(questionEntity);

        return questionEntity;
    }

    public Question Update(UpdateQuestionDTO updateQuestionDto)
    { 
        var checkQuestion = _repository.Question.GetQuestionById(updateQuestionDto.QuestionId);
        
        checkQuestion.QuestionText = updateQuestionDto.QuestionText;
            
        _repository.Question.UpdateQuestion(checkQuestion);

        return checkQuestion;
    }

    public void Delete(Guid id)
    {
        var checkQuestion = _repository.Question.GetQuestionById(id);
        
        _repository.Question.DeleteQuestion(checkQuestion);
    }
}