using FluentValidation;
using MediatR;

namespace Application.Commands.V1.Genres.CreateGenre;

public readonly record struct CreateGenreCommand(string Name) : IRequest<CreateGenreCommandResponse>;

public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
{
    public CreateGenreCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
    }
}