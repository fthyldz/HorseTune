using MediatR;

namespace Application.Commands.V1.Artists.CreateArtist;

public readonly record struct CreateArtistCommand(string Name, string Biography, int FormationYear) : IRequest<CreateArtistCommandResponse>;