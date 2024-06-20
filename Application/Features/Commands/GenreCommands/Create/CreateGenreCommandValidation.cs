
using FluentValidation;

namespace Application.Features.Commands.GenreCommands.Create;

public class CreateGenreCommandValidation : AbstractValidator<CreateGenreCommand>
{
    public CreateGenreCommandValidation()
    {
        RuleFor(c => c.artistId).NotEmpty().NotNull();
        RuleFor(c => c.createGenre.Name).NotEmpty().NotNull();
    }
}
