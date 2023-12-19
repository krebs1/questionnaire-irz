using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

[Table("variant")]
public class Variant
{
    [Column("id_variant")]
    public Guid VariantId { get; set; }

    [Column("is_correct_variant")]
    public bool VariantIsCorrect { get; set; }
    
    [Column("text_variant")]
    public string? VariantText { get; set; }
    
    [ForeignKey(nameof(Question))]
    public Guid QuestionId { get; set; }
    public Question? Question { get; set; }
}