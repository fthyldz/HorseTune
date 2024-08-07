using Domain.Artists;
using Domain.Songs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class SongConfiguration : IEntityTypeConfiguration<Song>
{
    public void Configure(EntityTypeBuilder<Song> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id)
            .HasConversion(
                songId => songId.Value,
                value => new SongId(value)
            );
        builder.Property(s => s.Title).IsRequired().HasMaxLength(100);
        builder.OwnsOne(s => s.Duration, d =>
        {
            d.Property(p => p.Minutes).IsRequired();
            d.Property(p => p.Seconds).IsRequired();
        });
        
        builder.HasOne(s => s.Album)
            .WithMany(a => a.Songs)
            .HasForeignKey(s => s.AlbumId);
    }
}