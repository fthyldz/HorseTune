namespace Application.Queries.V1.Genres.GetGenreById;

public readonly record struct GetGenreByIdQueryResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; }
}