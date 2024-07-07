using MediatR;
using Application.Dto;
using Domain.Models;
using Application.Interfaces.IRepository;
using Microsoft.Extensions.Logging;

namespace Application.Features.Queries.Song.GetSong;

public class GetSongByIdQuery : IRequest<SongDto>
{
    public int SongId { get; set; }
}

public class GetSongByIdQueryHandler : IRequestHandler<GetSongByIdQuery, SongDto>
{
    private readonly ILogger<GetSongByIdQueryHandler> _logger;
    private readonly ISongRepository _songRepository;

    public GetSongByIdQueryHandler(
        ILogger<GetSongByIdQueryHandler> logger,
        ISongRepository songRepository)
    {
        _logger = logger;
        _songRepository = songRepository;
    }

    public async Task<SongDto> Handle(GetSongByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var song = await _songRepository.GetSong(request.SongId, cancellationToken);

            if(song == null)
            {
                _logger.LogError("Song not found");
                throw new ArgumentNullException(nameof(song));
            }

            var response = new SongDto
            {
                Id = song.Id,
                Title = song.Title,
                Duration = song.Duration,
                ReleaseDate = song.ReleaseDate
            };

            _logger.LogInformation("Song taken {Id}", song.Id);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "an error occured during getting song/s");
            throw;
        }

    }
}
