namespace Application.Commands.V1.Artists.CreateArtist;

public readonly record struct CreateArtistCommandResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string? Biography { get; init; }
    public int FormationYear { get; init; }
}