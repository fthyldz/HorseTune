using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Application.Commands.V1.Artists.CreateArtist.DTOs;

public readonly record struct CreateArtistRequestDto
{
    [Required]
    public required string Name { get; init; }
    public string? Biography { get; init; }
    [Required]
    public required int FormationYear { get; init; }
}

public class CreateArtistRequestDtoValidator : AbstractValidator<CreateArtistRequestDto>
{
    public CreateArtistRequestDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(250);
        RuleFor(x => x.Biography).MaximumLength(5000);
        RuleFor(x => x.FormationYear).NotEmpty();
    }
}