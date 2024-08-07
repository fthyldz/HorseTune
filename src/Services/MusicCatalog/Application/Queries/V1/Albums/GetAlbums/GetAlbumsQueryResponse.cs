namespace Application.Queries.V1.Albums.GetAlbums;

public readonly record struct GetAlbumsQueryResponse
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public int ReleaseYear { get; init; }
}