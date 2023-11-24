using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notes.Domain.Notes;

namespace Notes.Infrastructure.Configurations;

internal class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title).HasMaxLength(100);

        builder.HasMany(x => x.Texts)
            .WithOne()
            .HasForeignKey(x => x.NoteId);

        builder.HasMany(x => x.Checkboxes)
            .WithOne()
            .HasForeignKey(x => x.NoteId);
    }
}
