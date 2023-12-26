using Entities.Models;
using questionnaire.DTO;

namespace questionnaire.Contracts;

public interface IQuestionnaireService
{
    IEnumerable<Questionnaire> GetAll();
    Questionnaire GetById(Guid id);
    Questionnaire Create(CreateQuestionnaireDTO createQuestionnaireDto);
    Questionnaire Update(UpdateQuestionnaireDTO updateQuestionnaireDto);
    void Delete(Guid id);
}