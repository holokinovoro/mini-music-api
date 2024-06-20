using FluentValidation;

namespace Application.Features.Queries.Artist;

public class GetArtistBySongValidation : AbstractValidator<GetArtistBySongIdQuery>
{
    public GetArtistBySongValidation()
    {
        RuleFor(a => a.SongId).NotEmpty();
    }
}
