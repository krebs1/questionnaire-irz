using AutoMapper;
using Contracts;
using Entities.Models;
using questionnaire.Contracts;
using questionnaire.DTO;

namespace questionnaire.Services;

public class QuestionnaireService : IQuestionnaireService
{
    private IRepositoryWrapper _repository;
    private IMapper _mapper;
    
    public QuestionnaireService (IRepositoryWrapper repository, IMapper mapper) 
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public IEnumerable<Questionnaire> GetAll()
    {
        return _repository.Questionnaire.GetAllQuestionnaire();
    }

    public Questionnaire GetById(Guid id)
    {
        return _repository.Questionnaire.GetQuestionnaireById(id);
    }

    public Questionnaire Create(CreateQuestionnaireDTO createQuestionnaireDto)
    {
        var questionnaireEntity = _mapper.Map<Questionnaire>(createQuestionnaireDto);
        _repository.Questionnaire.CreateQuestionnaire(questionnaireEntity);

        return questionnaireEntity;
    }

    public Questionnaire Update(UpdateQuestionnaireDTO updateQuestionnaireDto)
    {
        var questionnaireEntity = _mapper.Map<Questionnaire>(updateQuestionnaireDto);
        _repository.Questionnaire.UpdateQuestionnaire(questionnaireEntity);

        return questionnaireEntity;
    }

    public void Delete(Guid id)
    {
        var questionnaire = _repository.Questionnaire.GetQuestionnaireById(id);
        _repository.Questionnaire.DeleteQuestionnaire(questionnaire);
    }
}