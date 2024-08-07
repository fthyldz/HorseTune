namespace Application.Commands.V1.Albums.CreateAlbum.DTOs;

public readonly record struct CreateAlbumResponseDto
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public int ReleaseYear { get; init; }
}