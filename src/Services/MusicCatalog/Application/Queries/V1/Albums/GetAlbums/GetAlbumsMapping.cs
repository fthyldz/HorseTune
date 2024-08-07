using Application.Queries.V1.Albums.GetAlbums.DTOs;
using AutoMapper;
using Domain.Albums;

namespace Application.Queries.V1.Albums.GetAlbums;

public class GetAlbumsMapping : Profile
{
    public GetAlbumsMapping()
    {
        CreateMap<Album, GetAlbumsQueryResponse>().ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.Value));
        CreateMap<GetAlbumsQueryResponse, GetAlbumsResponseDto>();
    }
}