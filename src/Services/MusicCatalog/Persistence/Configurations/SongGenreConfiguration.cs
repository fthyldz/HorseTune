using Domain.Common;
using Domain.Genres;
using Domain.Songs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class SongGenreConfiguration : IEntityTypeConfiguration<SongGenre>
{
    public void Configure(EntityTypeBuilder<SongGenre> builder)
    {
        builder.HasKey(sg => new { sg.SongId, sg.GenreId });
        
        builder.Property(sg => sg.SongId)
            .HasConversion(
                songId => songId.Value,
                value => new SongId(value));
        
        builder.Property(sg => sg.GenreId)
            .HasConversion(
                genreId => genreId.Value,
                value => new GenreId(value));
        
        builder.HasOne(sg => sg.Song)
            .WithMany(s => s.Genres)
            .HasForeignKey(sg => sg.SongId);
        
        builder.HasOne(sg => sg.Genre)
            .WithMany(g => g.Songs)
            .HasForeignKey(sg => sg.GenreId);
    }
}