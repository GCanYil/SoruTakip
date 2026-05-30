using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SoruTakip.Models;

namespace SoruTakip.Data;

public class AppDbContext: IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Topic> Topics { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Folder> Folders { get; set; }
    public DbSet<QuestionFolder> QuestionFolders { get; set; }
    public DbSet<Analysis> Analyses { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<QuestionFolder>()
            .HasKey(qf=> new {qf.QuestionId, qf.FolderId });
        
        builder.Entity<QuestionFolder>()
            .HasOne(qf=> qf.Question)
            .WithMany(q=>q.QuestionFolders)
            .HasForeignKey(qf => qf.QuestionId);

        builder.Entity<QuestionFolder>()
            .HasOne(qf => qf.Folder)
            .WithMany(f => f.QuestionFolders)
            .HasForeignKey(qf => qf.FolderId);
    }
}