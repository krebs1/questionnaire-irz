namespace questionnaire.DTO;

public class AnswerFreeQuestionDTO
{
    public Guid WalkthroughId { get; set; }
    public Guid QuestionId { get; set; }
    public string Answer { get; set; }
}