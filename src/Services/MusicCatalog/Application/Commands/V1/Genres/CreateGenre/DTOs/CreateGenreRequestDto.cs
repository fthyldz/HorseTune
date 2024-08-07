using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Application.Commands.V1.Genres.CreateGenre.DTOs;

public readonly record struct CreateGenreRequestDto
{
    [Required]
    public required string Name { get; init; }
}

public class CreateGenreRequestDtoValidator : AbstractValidator<CreateGenreRequestDto>
{
    public CreateGenreRequestDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
    }
}