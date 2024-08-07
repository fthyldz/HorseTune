using Application.Queries.V1.Genres.GetGenreById.DTOs;
using AutoMapper;
using Domain.Genres;

namespace Application.Queries.V1.Genres.GetGenreById;

public class GetGenreByIdMapping : Profile
{
    public GetGenreByIdMapping()
    {
        CreateMap<Genre, GetGenreByIdQueryResponse>().ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.Value));
        CreateMap<GetGenreByIdQueryResponse, GetGenreByIdResponseDto>();
    }
}