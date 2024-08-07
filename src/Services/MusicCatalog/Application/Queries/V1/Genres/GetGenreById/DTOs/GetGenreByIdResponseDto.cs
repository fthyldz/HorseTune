namespace Application.Queries.V1.Genres.GetGenreById.DTOs;

public readonly record struct GetGenreByIdResponseDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
}