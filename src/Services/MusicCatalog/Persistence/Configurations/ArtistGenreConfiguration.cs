using Domain.Artists;
using Domain.Common;
using Domain.Genres;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class ArtistGenreConfiguration : IEntityTypeConfiguration<ArtistGenre>
{
    public void Configure(EntityTypeBuilder<ArtistGenre> builder)
    {
        builder.HasKey(ag => new { ag.ArtistId, ag.GenreId });
        
        builder.Property(ag => ag.ArtistId)
            .HasConversion(
                artistId => artistId.Value,
                value => new ArtistId(value));
        
        builder.Property(ag => ag.GenreId)
            .HasConversion(
                genreId => genreId.Value,
                value => new GenreId(value));
        
        builder.HasOne(ag => ag.Artist)
            .WithMany(a => a.Genres)
            .HasForeignKey(ag => ag.ArtistId);
        
        builder.HasOne(ag => ag.Genre)
            .WithMany(g => g.Artists)
            .HasForeignKey(ag => ag.GenreId);
    }
}