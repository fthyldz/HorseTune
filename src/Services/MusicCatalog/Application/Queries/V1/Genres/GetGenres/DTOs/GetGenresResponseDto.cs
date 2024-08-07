namespace Application.Queries.V1.Genres.GetGenres.DTOs;

public readonly record struct GetGenresResponseDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
}