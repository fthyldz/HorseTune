using Application.Abstractions.Data.Common;
using Domain.Albums;
using Domain.Artists;

namespace Application.Abstractions.Data.Repositories;

public interface IAlbumRepository : IGenericRepository<Album, AlbumId>
{
    Task<IReadOnlyList<Album>> GetAlbumsWithArtistIdAsync(ArtistId artistId, CancellationToken cancellationToken = default);
    Task<Album?> GetAlbumsWithArtistIdAndAlbumIdAsync(ArtistId artistId, AlbumId albumId, CancellationToken cancellationToken = default);
}