using Domain.Albums;
using Domain.Artists;
using Domain.Common;
using Domain.Genres;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class ArtistAlbumConfiguration : IEntityTypeConfiguration<ArtistAlbum>
{
    public void Configure(EntityTypeBuilder<ArtistAlbum> builder)
    {
        builder.HasKey(aa => new { aa.ArtistId, aa.AlbumId });
        
        builder.Property(aa => aa.ArtistId)
            .HasConversion(
                artistId => artistId.Value,
                value => new ArtistId(value));
        
        builder.Property(aa => aa.AlbumId)
            .HasConversion(
                albumId => albumId.Value,
                value => new AlbumId(value));
        
        builder.HasOne(aa => aa.Artist)
            .WithMany(a => a.Albums)
            .HasForeignKey(aa => aa.ArtistId);
        
        builder.HasOne(aa => aa.Album)
            .WithMany(g => g.Artists)
            .HasForeignKey(aa => aa.AlbumId);
    }
}