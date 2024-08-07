using Application.Queries.V1.Artists.GetArtists.DTOs;
using AutoMapper;
using Domain.Artists;

namespace Application.Queries.V1.Artists.GetArtists;

public class GetArtistsMapping : Profile
{
    public GetArtistsMapping()
    {
        CreateMap<Artist, GetArtistsQueryResponse>().ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.Value));
        CreateMap<GetArtistsQueryResponse, GetArtistsResponseDto>();
    }
}