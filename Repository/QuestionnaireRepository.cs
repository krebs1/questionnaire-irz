using Contracts;
using Entities;
using Entities.Models;

namespace Repository;

public class QuestionnaireRepository : RepositoryBase<Questionnaire>, IQuestionnaireRepository
{
    public QuestionnaireRepository(RepositoryContext repositoryContext)
        :base(repositoryContext){}

    public void CreateQuestionnaire(Questionnaire questionnaire)
    {
        Create(questionnaire);
    }

    public void UpdateQuestionnaire(Questionnaire questionnaire)
    {
       Update(questionnaire);
    }

    public void DeleteQuestionnaire(Questionnaire questionnaire)
    {
       Delete(questionnaire);
    }

    public Questionnaire GetQuestionnaireById(Guid questionnaireId)
    {
        return FindByCondition(questionnaire => questionnaire.QuestionnaireId.Equals(questionnaireId))
            .FirstOrDefault();
    }

    public IEnumerable<Questionnaire> GetAllQuestionnaire()
    {
        return FindAll()
            .ToList();
    }
}