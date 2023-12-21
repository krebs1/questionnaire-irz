using Entities.Models;

namespace Contracts;

public interface IQuestionnaireRepository
{
    void CreateQuestionnaire(Questionnaire questionnaire);
    void UpdateQuestionnaire(Questionnaire questionnaire);
    void DeleteQuestionnaire(Questionnaire questionnaire);
    Questionnaire GetQuestionnaireById(Guid questionnaireId);
    IEnumerable<Questionnaire> GetAllQuestionnaire();
}