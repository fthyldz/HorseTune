namespace Application.Queries.V1.Genres.GetGenres;

public readonly record struct GetGenresQueryResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; }
}