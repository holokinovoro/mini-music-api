using MediatR;
using Application.Dto;
using Application.Interfaces.IRepository;
using Microsoft.Extensions.Logging;

namespace Application.Features.Queries.Song.GetSong;

public class GetSongsByArtist : IRequest<List<SongDto>>
{
    public int ArtistId { get; set; }
}

public class GetSongsByArtistHandler : IRequestHandler<GetSongsByArtist, List<SongDto>>
{
    private readonly ILogger<GetSongsByArtistHandler> _logger;
    private readonly ISongRepository _songRepository;

    public GetSongsByArtistHandler(
        ILogger<GetSongsByArtistHandler> logger,
        ISongRepository songRepository)
    {
        _logger = logger;
        _songRepository = songRepository;
    }

    public async Task<List<SongDto>> Handle(GetSongsByArtist request, CancellationToken cancellationToken)
    {
        try
        {
            var songs = await _songRepository.GetSongsByArtist(request.ArtistId, cancellationToken);

            if (songs == null)
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
