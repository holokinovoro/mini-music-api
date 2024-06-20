
using FluentValidation;

namespace Application.Features.Commands.GenreCommands.Delete;

public class DeleteGenreCommandValidation : AbstractValidator<DeleteGenreCommand>
{
    public DeleteGenreCommandValidation()
    {
        RuleFor(d => d.GenreId).NotNull().NotEmpty();
    }
}
