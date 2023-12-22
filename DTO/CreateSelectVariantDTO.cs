namespace questionnaire.DTO;

public class CreateSelectVariantDTO
{
    public Guid VariantId { get; set; }
    public Guid WalkthroughQuestionId { get; set; }

    public CreateSelectVariantDTO(Guid variantId, Guid walkthroughQuestionId)
    {
        VariantId = variantId;
        WalkthroughQuestionId = walkthroughQuestionId;
    }
}