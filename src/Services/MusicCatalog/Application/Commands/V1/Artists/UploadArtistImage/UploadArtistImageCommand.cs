using Domain.Artists;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Commands.V1.Artists.UploadArtistImage;

public readonly record struct UploadArtistImageCommand(ArtistId ArtistId, IFormFile Image) : IRequest<UploadArtistImageCommandResponse>;