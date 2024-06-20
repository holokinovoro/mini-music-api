using FluentValidation;

namespace Application.Features.Commands.ArtistCommands.Delete;

public class DeleteArtistCommandValidation : AbstractValidator<DeleteArtistCommand>
{
    public DeleteArtistCommandValidation()
    {
        RuleFor(delete => delete.Id).NotEmpty();
    }
}
