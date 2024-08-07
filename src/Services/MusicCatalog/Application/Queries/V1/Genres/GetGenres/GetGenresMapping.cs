using Application.Queries.V1.Genres.GetGenres.DTOs;
using AutoMapper;
using Domain.Genres;

namespace Application.Queries.V1.Genres.GetGenres;

public class GetGenresMapping : Profile
{
    public GetGenresMapping()
    {
        CreateMap<Genre, GetGenresQueryResponse>().ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.Value));
        CreateMap<GetGenresQueryResponse, GetGenresResponseDto>();
    }
}