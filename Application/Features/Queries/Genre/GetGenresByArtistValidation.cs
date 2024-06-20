using FluentValidation;

namespace Application.Features.Queries.Genre;

public class GetGenresByArtistValidation : AbstractValidator<GetGenresByArtistQuery>
{
    public GetGenresByArtistValidation()
    {
        RuleFor(g => g.artistId).NotEmpty();
    }
}
