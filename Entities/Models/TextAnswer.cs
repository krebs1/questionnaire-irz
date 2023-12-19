using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

[Table("text_answer")]
public class TextAnswer
{
    [Column("id_text_answer")]
    public Guid TextAnswerId { get; set; }
    
    [Column("text_text_answer")]
    public string? TextAnswerText { get; set; }
    
    [Column("id_walkthrough_question")]
    [ForeignKey(nameof(WalkthroughQuestion))]
    public Guid WalkthroughQuestionId { get; set; }
    public WalkthroughQuestion? WalkthroughQuestion { get; set; }
}