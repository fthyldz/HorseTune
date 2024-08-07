using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Application.Commands.V1.Albums.CreateAlbum.DTOs;

public record struct CreateAlbumRequestDto
{
    public Guid ArtistId { get; set; }
    [Required]
    public required string Title { get; init; }
    [Required]
    public required int ReleaseYear { get; init; }
}

public class CreateAlbumRequestDtoValidator : AbstractValidator<CreateAlbumRequestDto>
{
    public CreateAlbumRequestDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        RuleFor(x => x.ReleaseYear).NotEmpty();
    }
}