using Application.Dto;
using Application.Interfaces.IRepository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Queries.Song.GetSong;

public class GetSongsByGenreQuery : IRequest<List<SongDto>>
{
    public int GenreId { get; set; }
}

public class GetSongsByGenreQueryHandler : IRequestHandler<GetSongsByGenreQuery, List<SongDto>>
{
    private readonly ILogger<GetSongsByGenreQueryHandler> _logger;
    private readonly IGenreRepository _genreRepository;

    public GetSongsByGenreQueryHandler(
        ILogger<GetSongsByGenreQueryHandler> logger,
        IGenreRepository genreRepository)
    {
        _logger = logger;
        _genreRepository = genreRepository;
    }
    public async Task<List<SongDto>> Handle(GetSongsByGenreQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var songs = await _genreRepository.GetSongsByGenre(request.GenreId, cancellationToken);

            if(songs == null)
            {
                _logger.LogError("Songs not found");
                throw new ArgumentNullException(nameof(songs));
            }
            var response = songs.Select(s => new SongDto
            {
                Id = s.Id,
                Title = s.Title,
                Duration = s.Duration,
                ReleaseDate = s.ReleaseDate
            }).ToList();

            _logger.LogInformation("Songs taken");
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "an error occured during getting song/s");
            throw;
        }
    }
}
