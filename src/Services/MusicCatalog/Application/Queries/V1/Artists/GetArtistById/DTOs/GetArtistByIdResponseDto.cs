namespace Application.Queries.V1.Artists.GetArtistById.DTOs;

public readonly record struct GetArtistByIdResponseDto
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string? Biography { get; init; }
    public int FormationYear { get; init; }
    public string? ImageUrl { get; init; }
}