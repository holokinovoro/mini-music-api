using FluentValidation;

namespace Application.Features.Queries.Song.GetSong;

public class GetSongsByArtistValidation : AbstractValidator<GetSongsByArtist>
{
    public GetSongsByArtistValidation()
    {
        RuleFor(s => s.ArtistId).NotEmpty();
    }
}
