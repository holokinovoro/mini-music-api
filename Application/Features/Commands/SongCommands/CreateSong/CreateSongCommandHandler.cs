using MediatR;
using Domain;
using Domain.Models;
using Application.Interfaces.IRepository;
using Microsoft.Extensions.Logging;

namespace Application.Features.Commands.SongCommands.CreateSong;

public class CreateSongCommandHandler : IRequestHandler<CreateSongCommand, int>
{
    private readonly ILogger<CreateSongCommandHandler> _logger;
    private readonly ISongRepository _songRepository;
    private readonly IArtistRepository _artistRepository;

    public CreateSongCommandHandler(
        ILogger<CreateSongCommandHandler> logger,
        ISongRepository songRepository,
        IArtistRepository artistRepository)
    {
        _songRepository = songRepository ?? throw new ArgumentNullException(nameof(songRepository));
        _artistRepository = artistRepository ?? throw new ArgumentNullException(nameof(artistRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<int> Handle(CreateSongCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var artist = await _artistRepository.GetArtist(request.artistId, cancellationToken);
            if (artist == null)
            {
                _logger.LogError("Not found");
                return 0;
            }


            var song = new Song
            {
                Id = request.createSong.Id,
                Title = request.createSong.Title,
                Artist = artist,
                Duration = request.createSong.Duration,
                ReleaseDate = request.createSong.ReleaseDate
            };

            await _songRepository.CreateSong(song, cancellationToken);
            _logger.LogInformation("Song Created {Id}", song.Id);

            return song.Id;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "an error occured during creating song");
            throw;
        }
    }
}