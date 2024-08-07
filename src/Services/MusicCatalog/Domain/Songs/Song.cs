using Domain.Albums;
using Domain.Common;
using Domain.Primitives;

namespace Domain.Songs;

public class Song : BaseEntity<SongId>
{
    private List<SongGenre> _genres = [];
    private List<SongArtist> _artists = [];
    public string Title { get; private set; }
    public Duration Duration { get; private set; }
    public IReadOnlyList<SongGenre> Genres => _genres.AsReadOnly();
    public IReadOnlyList<SongArtist> Artists => _artists.AsReadOnly();
    public AlbumId AlbumId { get; private set; }
    public Album Album { get; private set; }
    
    private Song()
    {
    }

    public Song(SongId id, string title, Duration duration, AlbumId albumId)
    {
        Id = id;
        Title = title;
        Duration = duration;
        AlbumId = albumId;
    }
}