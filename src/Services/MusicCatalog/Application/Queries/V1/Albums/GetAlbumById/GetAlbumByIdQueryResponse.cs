namespace Application.Queries.V1.Albums.GetAlbumById;

public readonly record struct GetAlbumByIdQueryResponse
{
    public Guid Id { get; init; }
    public string Title { get; init; }
}