using MediatR;

namespace Application.Queries.V1.Genres.GetGenres;

public readonly record struct GetGenresQuery: IRequest<IEnumerable<GetGenresQueryResponse>>;