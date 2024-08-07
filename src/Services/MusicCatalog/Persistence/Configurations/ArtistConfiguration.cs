using Domain.Artists;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
{
    public void Configure(EntityTypeBuilder<Artist> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id)
            .HasConversion(
                artistId => artistId.Value,
                value => new ArtistId(value)
            );
        builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
        builder.Property(a => a.Biography).HasMaxLength(3000);
        builder.Property(a => a.FormationYear).IsRequired();
    }
}