using Application.Commands.V1.Artists.CreateArtist.DTOs;
using AutoMapper;
using Domain.Artists;

namespace Application.Commands.V1.Artists.CreateArtist;

public class CreateArtistMapping : Profile
{
    public CreateArtistMapping()
    {
        CreateMap<CreateArtistRequestDto, CreateArtistCommand>();
        CreateMap<CreateArtistCommand, Artist>()
            .ConstructUsing(s => new Artist(new ArtistId(Guid.NewGuid()), s.Name, s.Biography, s.FormationYear));
        CreateMap<Artist, CreateArtistCommandResponse>().ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.Value));
        CreateMap<CreateArtistCommandResponse, CreateArtistResponseDto>();
    }
}