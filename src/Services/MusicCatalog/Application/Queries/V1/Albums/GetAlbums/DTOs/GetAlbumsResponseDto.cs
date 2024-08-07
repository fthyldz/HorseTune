namespace Application.Queries.V1.Albums.GetAlbums.DTOs;

public readonly record struct GetAlbumsResponseDto
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public int ReleaseYear { get; init; }
}