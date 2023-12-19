using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

[Table("question")]
public class Question
{
    [Column("id_question")]
    public Guid QuestionId { get; set; }

    [Column("type_question")]
    public string? QuestionType { get; set; }
    
    [Column("text_question")]
    public string? QuestionText { get; set; }
    
    [ForeignKey(nameof(Questionnaire))]
    public Guid QuestionnaireId { get; set; }
    public Questionnaire? Questionnaire { get; set; }
}