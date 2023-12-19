using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities;

public class RepositoryContext : DbContext
{
    public RepositoryContext (DbContextOptions options)
        :base(options){}
    
    public DbSet<Questionnaire>? Questionnaires { get; set; }
    public DbSet<Question>? Questions { get; set; }
    public DbSet<Variant>? Variants { get; set; }
    public DbSet<Walkthrough>? Walkthroughs { get; set; }
    public DbSet<WalkthroughQuestion>? WalkthroughQuestions { get; set; }
    public DbSet<TextAnswer>? TextAnswers { get; set; }
    public DbSet<SelectedVariant>? SelectedVariants { get; set; }
}