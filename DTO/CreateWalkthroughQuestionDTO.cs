namespace questionnaire.DTO;

public class CreateWalkthroughQuestionDTO
{
    public Guid WalkthroughId { get; set; }
    public Guid QuestionId { get; set; }

    public CreateWalkthroughQuestionDTO(Guid walkthroughId, Guid questionId)
    {
        WalkthroughId = walkthroughId;
        QuestionId = questionId;
    }
}