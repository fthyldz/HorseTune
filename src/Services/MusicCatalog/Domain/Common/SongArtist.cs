using Domain.Artists;
using Domain.Songs;

namespace Domain.Common;

public class SongArtist
{
    public SongId SongId { get; private set; }
    public Song Song { get; private set; }
    
    public ArtistId ArtistId { get; private set; }
    public Artist Artist { get; private set; }
    
    private SongArtist()
    {
    }
    
    public SongArtist(SongId songId, ArtistId artistId)
    {
        SongId = songId;
        ArtistId = artistId;
    }
}