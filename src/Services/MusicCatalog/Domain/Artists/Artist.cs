using Domain.Albums;
using Domain.Common;
using Domain.Events;
using Domain.Genres;
using Domain.Primitives;

namespace Domain.Artists;

public class Artist : BaseEntity<ArtistId>, IAggregateRoot
{
    private List<ArtistGenre> _genres = [];
    private List<ArtistAlbum> _albums = [];
    private List<SongArtist> _songs = [];
    public string Name { get; private set; }
    public string Biography { get; private set; }
    public int FormationYear { get; private set; }
    public string? ImageUrl { get; private set; }
    public IReadOnlyList<ArtistGenre> Genres => _genres.AsReadOnly();
    public IReadOnlyList<ArtistAlbum> Albums => _albums.AsReadOnly();
    public IReadOnlyList<SongArtist> Songs => _songs.AsReadOnly();
    
    private Artist()
    {
    }
    
    public Artist(ArtistId id, string name, string biography, int formationYear)
    {
        Id = id;
        Name = name;
        Biography = biography;
        FormationYear = formationYear;
    }
    
    public void AddGenre(GenreId genreId)
    {
        _genres.Add(new ArtistGenre(Id, genreId));
    }
    
    public void AddAlbum(AlbumId albumId)
    {
        _albums.Add(new ArtistAlbum(Id, albumId));
    }
    
    public void UploadImage(string path)
    {
        ImageUrl = path;
        AddEvent(new ArtistImageUploadedEvent(path));
    }
}