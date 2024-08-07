using Domain.Artists;
using Domain.Genres;

namespace Domain.Common;

public class ArtistGenre
{
    public ArtistId ArtistId { get; private set; }
    public Artist Artist { get; private set; }
    
    public GenreId GenreId { get; private set; }
    public Genre Genre { get; private set; }
    
    private ArtistGenre()
    {
    }
    
    public ArtistGenre(ArtistId artistId, GenreId genreId)
    {
        ArtistId = artistId;
        GenreId = genreId;
    }
}