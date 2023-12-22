namespace questionnaire.DTO;

public class AnswerSelectQuestionDTO
{
    public Guid WalkthroughId { get; set; }
    public Guid QuestionId { get; set; }
    public List<Guid> SelectedVariants { get; set; }
}