using Domain.Artists;
using MediatR;

namespace Application.Commands.V1.Albums.CreateAlbum;

public readonly record struct CreateAlbumCommand(Guid artistId, string Title, int ReleaseYear)
    : IRequest<CreateAlbumCommandResponse>
{
    public ArtistId ArtistId { get; init; } = new(artistId);
    public string Title { get; init;} = Title;
    public int ReleaseYear { get; init; } = ReleaseYear;
}