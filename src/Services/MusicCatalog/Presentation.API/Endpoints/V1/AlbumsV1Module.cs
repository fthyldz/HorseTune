using Application.Commands.V1.Albums.CreateAlbum;
using Application.Commands.V1.Albums.CreateAlbum.DTOs;
using Application.Constants;
using Application.Models.Common;
using Application.Queries.V1.Albums.GetAlbumById;
using Application.Queries.V1.Albums.GetAlbumById.DTOs;
using Application.Queries.V1.Albums.GetAlbums;
using Application.Queries.V1.Albums.GetAlbums.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.API.Filters;

namespace Presentation.API.Endpoints.V1;

public static class AlbumsV1Module
{
    public static RouteGroupBuilder RegisterAlbumsV1Endpoints(this RouteGroupBuilder group)
    {
        group.MapPost("/",
                async ([FromBody] CreateAlbumRequestDto request, [FromRoute] Guid artistId, IMapper mapper, IMediator sender,
                    CancellationToken cancellationToken = default) =>
                {
                    request.ArtistId = artistId;
                    var response = await sender.Send(mapper.Map<CreateAlbumCommand>(request), cancellationToken);
                    return Results.CreatedAtRoute("GetAlbumById", new { artistId = artistId, albumId = response.Id }, new SuccessResponse<CreateAlbumResponseDto>(mapper.Map<CreateAlbumResponseDto>(response), Messages.RecordCreated));
                })
            .AddEndpointFilter<ValidationFilter<CreateAlbumRequestDto>>()
            .WithName("CreateAlbum");

        group.MapGet("/{albumId:guid}", async ([FromRoute] Guid artistId, [FromRoute] Guid albumId, IMapper mapper, IMediator sender,
            CancellationToken cancellationToken = default) =>
        {
            var response = await sender.Send(new GetAlbumByIdQuery(artistId, albumId), cancellationToken);
            var result = mapper.Map<GetAlbumByIdResponseDto>(response);
            return Results.Ok(new SuccessResponse<GetAlbumByIdResponseDto>(result, Messages.RecordFound));
        }).WithName("GetAlbumById");

        group.MapGet("/", async ([FromRoute] Guid artistId, IMapper mapper, IMediator sender,
            CancellationToken cancellationToken = default) =>
        {
            var response = await sender.Send(new GetAlbumsQuery(artistId), cancellationToken);
            var result = mapper.Map<IEnumerable<GetAlbumsResponseDto>>(response);
            return Results.Ok(new SuccessResponse<IEnumerable<GetAlbumsResponseDto>>(result, Messages.RecordsListed));
        }).WithName("GetAlbums");

        /*group.MapGroup("/{albumId:guid}/songs")
           .RegisterSongsV1Endpoints()
           .WithTags("Song Api");*/

        return group;
    }
}