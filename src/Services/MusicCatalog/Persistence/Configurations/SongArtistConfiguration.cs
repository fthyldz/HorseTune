using Domain.Artists;
using Domain.Common;
using Domain.Genres;
using Domain.Songs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class SongArtistConfiguration : IEntityTypeConfiguration<SongArtist>
{
    public void Configure(EntityTypeBuilder<SongArtist> builder)
    {
        builder.HasKey(sa => new { sa.SongId, sa.ArtistId });
        
        builder.Property(sa => sa.SongId)
            .HasConversion(
                songId => songId.Value,
                value => new SongId(value));
        
        builder.Property(sa => sa.ArtistId)
            .HasConversion(
                artistId => artistId.Value,
                value => new ArtistId(value));
        
        builder.HasOne(sa => sa.Song)
            .WithMany(s => s.Artists)
            .HasForeignKey(sa => sa.SongId);
        
        builder.HasOne(sa => sa.Artist)
            .WithMany(a => a.Songs)
            .HasForeignKey(sa => sa.ArtistId);
    }
}