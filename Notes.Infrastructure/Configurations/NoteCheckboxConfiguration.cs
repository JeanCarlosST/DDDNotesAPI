using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notes.Domain.Notes;

namespace Notes.Infrastructure.Configurations;

internal class NoteCheckboxConfiguration : IEntityTypeConfiguration<NoteCheckbox>
{
    public void Configure(EntityTypeBuilder<NoteCheckbox> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Order).IsRequired();
    }
}
