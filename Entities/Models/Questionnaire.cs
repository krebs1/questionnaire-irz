using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

[Table("questionnaire")]
public class Questionnaire
{
    [Column("id_questionnaire")]
    public Guid QuestionnaireId { get; set; }
    
    [Column("title_questionnaire")]
    public string? QuestionnaireTitle { get; set; }
}t