using Microsoft.EntityFrameworkCore;
using Notes.Domain.Labels;
using Notes.Domain.Notes;

namespace Notes.Infrastructure;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(AppDbContext).Assembly);
    }

    public DbSet<Note> Notes { get; set; }
    public DbSet<Label> Labels { get; set; }
    public DbSet<NoteText> NoteTexts { get; set; }
    public DbSet<NoteCheckbox> NoteCheckboxes { get; set; }
}
