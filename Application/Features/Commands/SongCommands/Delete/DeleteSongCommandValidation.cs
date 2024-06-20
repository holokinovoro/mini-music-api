
using FluentValidation;

namespace Application.Features.Commands.SongCommands.Delete;

public class DeleteSongCommandValidation : AbstractValidator<DeleteSongCommand>
{
    public DeleteSongCommandValidation()
    {
        RuleFor(s => s.Id).NotEmpty();
    }
}
