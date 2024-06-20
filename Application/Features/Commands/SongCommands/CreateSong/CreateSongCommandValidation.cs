using FluentValidation;

namespace Application.Features.Commands.SongCommands.CreateSong;

public class CreateSongCommandValidation : AbstractValidator<CreateSongCommand>
{
    public CreateSongCommandValidation()
    {
        RuleFor(s => s.artistId).NotEmpty().NotNull();
        RuleFor(s => s.createSong.Title).NotEmpty().NotNull();
    }
}
