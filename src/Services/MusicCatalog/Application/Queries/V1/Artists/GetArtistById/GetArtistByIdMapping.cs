using Application.Queries.V1.Artists.GetArtistById.DTOs;
using AutoMapper;
using Domain.Artists;

namespace Application.Queries.V1.Artists.GetArtistById;

public class GetArtistByIdMapping : Profile
{
    public GetArtistByIdMapping()
    {
        CreateMap<Artist, GetArtistByIdQueryResponse>().ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.Value));
        CreateMap<GetArtistByIdQueryResponse, GetArtistByIdResponseDto>();
    }
}