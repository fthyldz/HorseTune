using Application.Queries.V1.Albums.GetAlbumById.DTOs;
using AutoMapper;
using Domain.Albums;

namespace Application.Queries.V1.Albums.GetAlbumById;

public class GetAlbumByIdMapping : Profile
{
    public GetAlbumByIdMapping()
    {
        CreateMap<Album, GetAlbumByIdQueryResponse>().ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.Value));
        CreateMap<GetAlbumByIdQueryResponse, GetAlbumByIdResponseDto>();
    }
}