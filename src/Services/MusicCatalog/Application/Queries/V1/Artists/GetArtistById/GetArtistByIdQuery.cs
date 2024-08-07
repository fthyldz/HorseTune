using Domain.Artists;
using MediatR;

namespace Application.Queries.V1.Artists.GetArtistById;

public readonly record struct GetArtistByIdQuery(Guid Value) : IRequest<GetArtistByIdQueryResponse>
{
    public ArtistId ArtistId { get; init; } = new ArtistId(Value);
}