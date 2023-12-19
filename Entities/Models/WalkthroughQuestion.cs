using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

[Table("walkthrough_question")]
public class WalkthroughQuestion
{
    [Column("id_walkthrough_question")]
    public Guid WalkthroughQuestionId { get; set; }
    
    [Column("id_walkthrough")]
    [ForeignKey(nameof(Walkthrough))]
    public Guid WalkthroughId { get; set; }
    public Walkthrough? Walkthrough { get; set; }
    
    [Column("id_question")]
    [ForeignKey(nameof(Question))]
    public Guid QuestionId { get; set; }
    public Question? Question { get; set; }
}