namespace Application.Queries.V1.Albums.GetAlbumById.DTOs;

public readonly record struct GetAlbumByIdResponseDto
{
    public Guid Id { get; init; }
    public string Title { get; init; }
}