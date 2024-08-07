using Domain.Genres;
using MediatR;

namespace Application.Queries.V1.Genres.GetGenreById;

public readonly record struct GetGenreByIdQuery(Guid Value) : IRequest<GetGenreByIdQueryResponse?>
{
    public GenreId GenreId { get; init; } = new GenreId(Value);
}