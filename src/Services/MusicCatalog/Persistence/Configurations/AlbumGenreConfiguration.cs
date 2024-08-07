using Domain.Albums;
using Domain.Common;
using Domain.Genres;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class AlbumGenreConfiguration : IEntityTypeConfiguration<AlbumGenre>
{
    public void Configure(EntityTypeBuilder<AlbumGenre> builder)
    {
        builder.HasKey(ag => new { ag.AlbumId, ag.GenreId });
        
        builder.Property(ag => ag.AlbumId)
            .HasConversion(
                albumId => albumId.Value,
                value => new AlbumId(value));
        
        builder.Property(ag => ag.GenreId)
            .HasConversion(
                genreId => genreId.Value,
                value => new GenreId(value));
        
        builder.HasOne(ag => ag.Album)
            .WithMany(a => a.Genres)
            .HasForeignKey(ag => ag.AlbumId);
        
        builder.HasOne(ag => ag.Genre)
            .WithMany(g => g.Albums)
            .HasForeignKey(ag => ag.GenreId);
    }
}