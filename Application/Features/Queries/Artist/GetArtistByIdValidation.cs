
using FluentValidation;

namespace Application.Features.Queries.Artist;

public class GetArtistByIdValidation : AbstractValidator<GetArtistByIdQuery>
{
    public GetArtistByIdValidation()
    {
        RuleFor(a => a.ArtistId).NotEmpty();
    }
}
