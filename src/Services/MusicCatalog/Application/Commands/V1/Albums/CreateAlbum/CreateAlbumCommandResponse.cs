namespace Application.Commands.V1.Albums.CreateAlbum;

public readonly record struct CreateAlbumCommandResponse
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public int ReleaseYear { get; init; }
}