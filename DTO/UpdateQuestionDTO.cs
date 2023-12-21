using System.ComponentModel;

namespace questionnaire.DTO;

public class UpdateQuestionDTO
{
    [ReadOnly(true)]
    public string QuestionId { get; set;  }
    
    public string QuestionText { get; set; }
    
    public string QuestionType { get; set; }
    
    public string QuestionnaireId { get; set; }
    
}