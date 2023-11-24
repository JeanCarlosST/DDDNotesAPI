using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notes.Domain.Labels;

namespace Notes.Infrastructure.Configurations;

internal class LabelConfiguration : IEntityTypeConfiguration<Label>
{
    public void Configure(EntityTypeBuilder<Label> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasMaxLength(50);

        builder.HasMany(x => x.Notes)
            .WithOne()
            .HasForeignKey(x => x.LabelId);
    }
}
