using MediatR;
using Application.Dto;
using Application.Interfaces.IRepository;
using Microsoft.Extensions.Logging;

namespace Application.Features.Queries.Song.GetSong;

public class GetAllSongsQuery : IRequest<List<SongDto>> { }


public class GetAllSongsQueryHandler : IRequestHandler<GetAllSongsQuery, List<SongDto>>
{
    private readonly ILogger<GetAllSongsQueryHandler> _logger;
    private readonly ISongRepository _songRepository;

    public GetAllSongsQueryHandler(
        ILogger<GetAllSongsQueryHandler> logger,
        ISongRepository songRepository)
    {
        _logger = logger;
        _songRepository = songRepository;
    }

    public async Task<List<SongDto>> Handle(GetAllSongsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var songs = await _songRepository.GetSongs(cancellationToken);

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

