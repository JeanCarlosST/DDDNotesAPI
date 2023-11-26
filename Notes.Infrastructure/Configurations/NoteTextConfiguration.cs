using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notes.Domain.Notes;

namespace Notes.Infrastructure.Configurations;

internal class NoteTextConfiguration : IEntityTypeConfiguration<NoteText>
{
    public void Configure(EntityTypeBuilder<NoteText> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Order).IsRequired();
    }
}