using Domain.Albums;
using Domain.Artists;

namespace Domain.Common;

public class ArtistAlbum
{
    public ArtistId ArtistId { get; private set; }
    public Artist Artist { get; private set; }
    
    public AlbumId AlbumId { get; private set; }
    public Album Album { get; private set; }
    
    private ArtistAlbum()
    {
    }
    
    public ArtistAlbum(ArtistId artistId, AlbumId albumId)
    {
        ArtistId = artistId;
        AlbumId = albumId;
    }
}