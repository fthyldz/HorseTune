using Application.Commands.V1.Albums.CreateAlbum.DTOs;
using AutoMapper;
using Domain.Albums;

namespace Application.Commands.V1.Albums.CreateAlbum;

public class CreateAlbumMapping : Profile
{
    public CreateAlbumMapping()
    {
        CreateMap<CreateAlbumRequestDto, CreateAlbumCommand>();
        CreateMap<CreateAlbumCommand, Album>()
            .ConstructUsing(s => new Album(new AlbumId(Guid.NewGuid()), s.Title, s.ReleaseYear));
        CreateMap<Album, CreateAlbumCommandResponse>().ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.Value));
        CreateMap<CreateAlbumCommandResponse, CreateAlbumResponseDto>();
    }
}