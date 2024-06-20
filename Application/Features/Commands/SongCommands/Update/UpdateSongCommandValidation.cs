
using FluentValidation;

namespace Application.Features.Commands.SongCommands.Update;

public class UpdateSongCommandValidation : AbstractValidator<UpdateSongCommand>
{
    public UpdateSongCommandValidation()
    {
        RuleFor(s => s.UpdateSong.Id).NotEmpty().NotNull();
        RuleFor(s => s.UpdateSong.Title).NotEmpty().NotNull();


    }
}
