
using FluentValidation;

namespace Application.Features.Commands.ArtistCommands.Update;

public class UpdateArtistCommandValidation : AbstractValidator<UpdateArtistCommand>
{
    public UpdateArtistCommandValidation()
    {
        RuleFor(u => u.ArtistUpdate.Id).NotEmpty();
        RuleFor(u => u.ArtistUpdate.Name).NotEmpty();
        RuleFor(u => u.GenreId).NotEmpty();
    }
}
