using Microsoft.EntityFrameworkCore;
using web_text_forum.Models;

public class ApplicationDbContext : DbContext
{
    public DbSet<Like> Likes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Like>()
            .HasIndex(l => new { l.PostId, l.UserId })
            .IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}