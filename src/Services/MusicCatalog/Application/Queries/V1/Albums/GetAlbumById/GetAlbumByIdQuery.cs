using Domain.Albums;
using Domain.Artists;
using MediatR;

namespace Application.Queries.V1.Albums.GetAlbumById;

public readonly record struct GetAlbumByIdQuery(Guid artistId, Guid albumId) : IRequest<GetAlbumByIdQueryResponse>
{
    public ArtistId ArtistId => new(artistId);
    public AlbumId AlbumId => new(albumId);
}