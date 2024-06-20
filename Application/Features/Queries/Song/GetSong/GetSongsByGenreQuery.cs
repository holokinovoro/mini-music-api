using Application.Dto;
using Application.Interfaces.IRepository;
using MediatR;

namespace Application.Features.Queries.Song.GetSong;

public class GetSongsByGenreQuery : IRequest<List<SongDto>>
{
    public int GenreId { get; set; }
}

public class GetSongsByGenreQueryHandler : IRequestHandler<GetSongsByGenreQuery, List<SongDto>>
{
    private readonly IGenreRepository _genreRepository;

    public GetSongsByGenreQueryHandler(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }
    public async Task<List<SongDto>> Handle(GetSongsByGenreQuery request, CancellationToken cancellationToken)
    {
        var songs = await _genreRepository.GetSongsByGenre(request.GenreId, cancellationToken);

        var response = songs.Select(s => new SongDto
        {
            Id = s.Id,
            Title = s.Title,
            Duration = s.Duration,
            ReleaseDate = s.ReleaseDate
        }).ToList();

        return response;
    }
}
