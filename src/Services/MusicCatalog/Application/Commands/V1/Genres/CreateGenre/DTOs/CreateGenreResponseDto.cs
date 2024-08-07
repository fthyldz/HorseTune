namespace Application.Commands.V1.Genres.CreateGenre.DTOs;

public readonly record struct CreateGenreResponseDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
}