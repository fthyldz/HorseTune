using Application.Commands.V1.Genres.CreateGenre;
using Application.Commands.V1.Genres.CreateGenre.DTOs;
using Application.Constants;
using Application.Models.Common;
using Application.Queries.V1.Genres.GetGenreById;
using Application.Queries.V1.Genres.GetGenreById.DTOs;
using Application.Queries.V1.Genres.GetGenres;
using Application.Queries.V1.Genres.GetGenres.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.API.Filters;

namespace Presentation.API.Endpoints.V1;

public static class GenresV1Module
{
    public static RouteGroupBuilder RegisterGenresV1Endpoints(this RouteGroupBuilder group)
    {
        group.MapPost("/",
                async ([FromBody] CreateGenreRequestDto request, IMapper mapper, IMediator sender,
                    CancellationToken cancellationToken = default) =>
                {
                    var response = await sender.Send(mapper.Map<CreateGenreCommand>(request), cancellationToken);
                    var result = mapper.Map<CreateGenreResponseDto>(response);
                    return Results.CreatedAtRoute("GetGenreById", new { genreId = response.Id },
                        new SuccessResponse<CreateGenreResponseDto>(result, Messages.RecordCreated));
                })
            .AddEndpointFilter<ValidationFilter<CreateGenreRequestDto>>()
            .WithName("CreateGenre");

        group.MapGet("/{genreId:guid}", async ([FromRoute] Guid genreId, IMapper mapper, IMediator sender,
            CancellationToken cancellationToken = default) =>
        {
            var response = await sender.Send(new GetGenreByIdQuery(genreId), cancellationToken);
            var result = mapper.Map<GetGenreByIdResponseDto>(response);
            return Results.Ok(new SuccessResponse<GetGenreByIdResponseDto>(result, Messages.RecordFound));
        }).WithName("GetGenreById");

        group.MapGet("/", async (IMapper mapper, IMediator sender,
            CancellationToken cancellationToken = default) =>
        {
            var response = await sender.Send(new GetGenresQuery(), cancellationToken);
            var result = mapper.Map<IEnumerable<GetGenresResponseDto>>(response);
            return Results.Ok(new SuccessResponse<IEnumerable<GetGenresResponseDto>>(result, Messages.RecordsListed));
        }).WithName("GetGenres");

        return group;
    }
}