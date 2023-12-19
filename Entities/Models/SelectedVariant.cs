using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

[Table("selected_variant")]
public class SelectedVariant
{
    [Column("id_selected_variant")]
    public Guid SelectedVariantId { get; set; }

    [Column("id_variant")]
    [ForeignKey(nameof(Variant))]
    public Guid VariantId { get; set; }
    public Variant? Variant { get; set; }
    
    [Column("id_walkthrough_question")]
    [ForeignKey(nameof(WalkthroughQuestion))]
    public Guid WalkthroughQuestionId { get; set; }
    public WalkthroughQuestion? WalkthroughQuestion { get; set; }
}