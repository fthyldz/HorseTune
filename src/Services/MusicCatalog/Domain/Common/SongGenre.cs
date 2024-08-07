using Domain.Genres;
using Domain.Songs;

namespace Domain.Common;

public class SongGenre
{
    public SongId SongId { get; private set; }
    public Song Song { get; private set; }
    
    public GenreId GenreId { get; private set; }
    public Genre Genre { get; private set; }
    
    private SongGenre()
    {
    }
    
    public SongGenre(SongId songId, GenreId genreId)
    {
        SongId = songId;
        GenreId = genreId;
    }
}