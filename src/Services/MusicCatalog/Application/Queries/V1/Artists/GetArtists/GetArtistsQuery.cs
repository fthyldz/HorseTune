using MediatR;

namespace Application.Queries.V1.Artists.GetArtists;

public readonly record struct GetArtistsQuery : IRequest<IEnumerable<GetArtistsQueryResponse>>;