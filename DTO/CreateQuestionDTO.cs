namespace questionnaire.DTO;

public class CreateQuestionDTO
{
    
    public string QuestionType { get; set; }
    
    public string QuestionText { get; set; }
    
    public Guid QuestionnaireId { get; set; }
}