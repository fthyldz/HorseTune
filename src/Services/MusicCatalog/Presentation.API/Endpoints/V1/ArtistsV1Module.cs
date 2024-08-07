using Application.Commands.V1.Artists.CreateArtist;
using Application.Commands.V1.Artists.CreateArtist.DTOs;
using Application.Commands.V1.Artists.UploadArtistImage;
using Application.Commands.V1.Artists.UploadArtistImage.DTOs;
using Application.Constants;
using Application.Models.Common;
using Application.Queries.V1.Artists.GetArtistById;
using Application.Queries.V1.Artists.GetArtistById.DTOs;
using Application.Queries.V1.Artists.GetArtists;
using Application.Queries.V1.Artists.GetArtists.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.API.Filters;

namespace Presentation.API.Endpoints.V1;

public static class ArtistsV1Module
{
    public static RouteGroupBuilder RegisterArtistsV1Endpoints(this RouteGroupBuilder group)
    {
        group.MapPost("/",
                async ([FromBody] CreateArtistRequestDto request, IMapper mapper, IMediator sender,
                    CancellationToken cancellationToken = default) =>
                {
                    var response = await sender.Send(mapper.Map<CreateArtistCommand>(request), cancellationToken);
                    return Results.CreatedAtRoute("GetArtistById", new { artistId = response.Id }, new SuccessResponse<CreateArtistResponseDto>(mapper.Map<CreateArtistResponseDto>(response), Messages.RecordCreated));
                })
            .AddEndpointFilter<ValidationFilter<CreateArtistRequestDto>>()
            .WithName("CreateArtist");

        group.MapGet("/{artistId:guid}", async ([FromRoute] Guid artistId, IMapper mapper, IMediator sender,
            CancellationToken cancellationToken = default) =>
        {
            var response = await sender.Send(new GetArtistByIdQuery(artistId), cancellationToken);
            var result = mapper.Map<GetArtistByIdResponseDto>(response);
            return Results.Ok(new SuccessResponse<GetArtistByIdResponseDto>(result, Messages.RecordFound));
        }).WithName("GetArtistById");

        group.MapGet("/", async (IMapper mapper, IMediator sender,
            CancellationToken cancellationToken = default) =>
        {
            var response = await sender.Send(new GetArtistsQuery(), cancellationToken);
            var result = mapper.Map<IEnumerable<GetArtistsResponseDto>>(response);
            return Results.Ok(new SuccessResponse<IEnumerable<GetArtistsResponseDto>>(result, Messages.RecordsListed));
        }).WithName("GetArtists");

        group.MapPost("/{artistId:guid}/image",
                async ([FromForm] IFormFile file, [FromRoute] Guid artistId, IMapper mapper, IMediator sender,
                    CancellationToken cancellationToken = default) =>
                {
                    if (file.Length == 0)
                    {
                        return Results.BadRequest("No file uploaded.");
                    }

                    var response =
                        await sender.Send(
                            mapper.Map<UploadArtistImageCommand>(new UploadArtistImageRequestDto(artistId, file)),
                            cancellationToken);
                    var result = mapper.Map<UploadArtistImageResponseDto>(response);
                    return Results.Ok(
                        new SuccessResponse<UploadArtistImageResponseDto>(result, Messages.RecordCreated));

                }).DisableAntiforgery()
            .WithName("UploadArtistImage");

        group.MapGroup("/{artistId:guid}/albums")
            .RegisterAlbumsV1Endpoints()
            .WithTags("Album Api");

        return group;
    }
}