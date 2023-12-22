using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Entities.Models;

[Table("walkthrough")]
public class Walkthrough
{
    [Column("id_walkthrough")]
    public Guid WalkthroughId { get; set; }

    [Column("start_walkthrough")]
    public DateTime WalkthroughStart { get; set; }
    
    [Column("end_walkthrough")]
    public DateTime? WalkthroughEnd { get; set; }
    
    [ForeignKey(nameof(Questionnaire))]
    public Guid QuestionnaireId { get; set; }
    public Questionnaire? Questionnaire { get; set; }
    
    [ForeignKey(nameof(IdentityUser))]
    public string? UserId { get; set; }
    
    public IdentityUser? IdentityUser { get; set; }
}