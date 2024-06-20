using FluentValidation;

namespace Application.Features.Queries.Artist;

public class GetArtistByGenreValidation : AbstractValidator<GetArtistsByGenre>
{
    public GetArtistByGenreValidation()
    {
        RuleFor(a => a.GenreId).NotEmpty();
    }
}
