namespace Application.Commands.V1.Artists.CreateArtist.DTOs;

public readonly record struct CreateArtistResponseDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string? Biography { get; init; }
    public int FormationYear { get; init; }
}