using Application.Commands.V1.Artists.UploadArtistImage.DTOs;
using AutoMapper;
using Domain.Artists;

namespace Application.Commands.V1.Artists.UploadArtistImage;

public class UploadArtistImageMapping : Profile
{
    public UploadArtistImageMapping()
    {
        CreateMap<UploadArtistImageRequestDto, UploadArtistImageCommand>()
            .ConstructUsing(s => new UploadArtistImageCommand(new ArtistId(Guid.NewGuid()), s.Image));
        CreateMap<UploadArtistImageCommandResponse, UploadArtistImageResponseDto>();
    }
}