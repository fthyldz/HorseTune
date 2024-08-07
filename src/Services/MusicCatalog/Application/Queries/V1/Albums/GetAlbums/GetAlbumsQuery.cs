using Domain.Artists;
using MediatR;

namespace Application.Queries.V1.Albums.GetAlbums;

public readonly record struct GetAlbumsQuery(Guid artistId) : IRequest<IEnumerable<GetAlbumsQueryResponse>>
{
    public ArtistId ArtistId => new(artistId);
}