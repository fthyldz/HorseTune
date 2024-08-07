using Domain.Albums;
using Domain.Artists;
using Domain.Common;
using Domain.Genres;
using Domain.Songs;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Artist> Artists { get; set; }
    public DbSet<ArtistGenre> ArtistGenres { get; set; }
    public DbSet<Album> Albums { get; set; }
    public DbSet<AlbumGenre> AlbumGenres { get; set; }
    public DbSet<ArtistAlbum> ArtistAlbums { get; set; }
    public DbSet<Song> Songs { get; set; }
    public DbSet<SongGenre> SongGenres { get; set; }
    public DbSet<SongArtist> SongArtists { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}