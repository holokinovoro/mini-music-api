
using FluentValidation;

namespace Application.Features.Commands.GenreCommands.Update;

public class UpdateGenreCommandValidation : AbstractValidator<UpdateGenreCommand>
{
    public UpdateGenreCommandValidation()
    {
        RuleFor(u => u.ArtistId).NotEmpty();
        RuleFor(u => u.Genre.Id).NotEmpty();
        RuleFor(u => u.Genre.Name).NotEmpty();
    }
}
