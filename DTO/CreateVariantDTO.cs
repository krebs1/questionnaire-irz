namespace questionnaire.DTO;

public class CreateVariantDTO
{
    public string VariantText { get; set; }
    
    public Guid QuestionId { get; set; }
}