using FluentValidation;

namespace Application.Features.Queries.Song.GetSong;

public class GetSongsByGenreValidation : AbstractValidator<GetSongsByGenreQuery>
{
    public GetSongsByGenreValidation()
    {
        RuleFor(s => s.GenreId).NotEmpty();
    }
}
