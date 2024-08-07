using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Commands.V1.Artists.UploadArtistImage.DTOs;

public readonly record struct UploadArtistImageRequestDto([property: Required] Guid ArtistId, [property: Required] IFormFile Image);

public class UploadArtistImageRequestDtoValidator : AbstractValidator<UploadArtistImageRequestDto>
{
    public UploadArtistImageRequestDtoValidator()
    {
        RuleFor(x => x.ArtistId).NotEmpty();
        RuleFor(x => x.Image).NotEmpty();
    }
}