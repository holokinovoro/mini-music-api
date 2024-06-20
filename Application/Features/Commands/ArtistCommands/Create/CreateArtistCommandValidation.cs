using FluentValidation;

namespace Application.Features.Commands.ArtistCommands.Create;

public class CreateArtistCommandValidation : AbstractValidator<CreateArtistCommand>
{
    public CreateArtistCommandValidation()
    {
        RuleFor(create =>
        create.GenreId).NotEmpty();
        RuleFor(create => create.CreateArtist.Name).NotEmpty();
    }
}
