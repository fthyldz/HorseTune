using Domain.Albums;
using Domain.Genres;

namespace Domain.Common;

public class AlbumGenre
{
    public AlbumId AlbumId { get; private set; }
    public Album Album { get; private set; }
    
    public GenreId GenreId { get; private set; }
    public Genre Genre { get; private set; }
    
    private AlbumGenre()
    {
    }
    
    public AlbumGenre(AlbumId albumId, GenreId genreId)
    {
        AlbumId = albumId;
        GenreId = genreId;
    }
}