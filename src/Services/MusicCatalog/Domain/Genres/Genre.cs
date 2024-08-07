using Domain.Common;
using Domain.Primitives;

namespace Domain.Genres;

public class Genre : BaseEntity<GenreId>
{
    private List<ArtistGenre> _artists = [];
    private List<AlbumGenre> _albums = [];
    private List<SongGenre> _songs = [];
    public string Name { get; private set; }
    public IReadOnlyList<ArtistGenre> Artists => _artists.AsReadOnly();
    public IReadOnlyList<AlbumGenre> Albums => _albums.AsReadOnly();
    public IReadOnlyList<SongGenre> Songs => _songs.AsReadOnly();
    
    private Genre()
    {
    }
    
    public Genre(GenreId id, string name)
    {
        Id = id;
        Name = name;
    }
}