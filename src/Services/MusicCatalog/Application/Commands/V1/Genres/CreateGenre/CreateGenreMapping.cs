using Application.Commands.V1.Genres.CreateGenre.DTOs;
using AutoMapper;
using Domain.Genres;

namespace Application.Commands.V1.Genres.CreateGenre;

public class CreateGenreMapping : Profile
{
    public CreateGenreMapping()
    {
        CreateMap<CreateGenreRequestDto, CreateGenreCommand>();
        CreateMap<CreateGenreCommand, Genre>()
            .ConstructUsing(s => new Genre(new GenreId(Guid.NewGuid()), s.Name));
        CreateMap<Genre, CreateGenreCommandResponse>().ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.Value));
        CreateMap<CreateGenreCommandResponse, CreateGenreResponseDto>();
    }
}