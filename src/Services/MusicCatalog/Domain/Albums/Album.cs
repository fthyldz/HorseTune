using Domain.Artists;
using Domain.Common;
using Domain.Genres;
using Domain.Primitives;
using Domain.Songs;

namespace Domain.Albums;

public class Album : BaseEntity<AlbumId>, IAggregateRoot
{
    private List<AlbumGenre> _genres = [];
    private List<ArtistAlbum> _artists = [];
    private List<Song> _songs = [];
    public string Title { get; private set; }
    public int ReleaseYear { get; private set; }
    public IReadOnlyList<AlbumGenre> Genres => _genres.AsReadOnly();
    public IReadOnlyList<ArtistAlbum> Artists => _artists.AsReadOnly();
    public IReadOnlyList<Song> Songs => _songs.AsReadOnly();

    private Album()
    {
    }
    
    public Album(AlbumId id, string title, int releaseYear)
    {
        Id = id;
        Title = title;
        ReleaseYear = releaseYear;
    }
    
    public void AddGenre(GenreId genreId)
    {
        _genres.Add(new AlbumGenre(Id, genreId));
    }
    
    public void AddArtist(ArtistId artistId)
    {
        _artists.Add(new ArtistAlbum(artistId, Id));
    }
    
    public void AddSong(Song song)
    {
        _songs.Add(song);
    }
}