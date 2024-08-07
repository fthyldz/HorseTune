namespace Application.Commands.V1.Genres.CreateGenre;

public readonly record struct CreateGenreCommandResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; }
}