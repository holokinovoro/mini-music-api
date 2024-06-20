using FluentValidation;

namespace Application.Features.Queries.Song.GetSong;

public class GetSongByIdValidation : AbstractValidator<GetSongByIdQuery>
{
    public GetSongByIdValidation()
    {
        RuleFor(s => s.SongId).NotEmpty();
    }
}
