using Domain.Albums;
using Domain.Artists;
using Domain.Songs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class AlbumConfiguration : IEntityTypeConfiguration<Album>
{
    public void Configure(EntityTypeBuilder<Album> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id)
            .HasConversion(
                albumId => albumId.Value,
                value => new AlbumId(value)
            );
        builder.Property(a => a.Title).IsRequired().HasMaxLength(100);
        builder.Property(a => a.ReleaseYear).IsRequired();
    }
}