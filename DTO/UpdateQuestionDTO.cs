using System.ComponentModel;

namespace questionnaire.DTO;

public class UpdateQuestionDTO
{
    public Guid QuestionId { get; set; }
    
    public string QuestionText { get; set; }
}