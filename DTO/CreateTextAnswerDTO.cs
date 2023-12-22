namespace questionnaire.DTO;

public class CreateTextAnswerDTO
{
    public Guid WalkthroughQuestionId { get; set; }
    public string? TextAnswerText { get; set; }

    public CreateTextAnswerDTO(Guid walkthroughQuestionId, string textAnswerText)
    {
        WalkthroughQuestionId = walkthroughQuestionId;
        TextAnswerText = textAnswerText;
    }
}