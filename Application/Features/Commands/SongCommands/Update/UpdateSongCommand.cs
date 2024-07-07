using MediatR;
using Application.Dto;
using Domain.Models;
using Application.Interfaces.IRepository;
using Microsoft.Extensions.Logging;

namespace Application.Features.Commands.SongCommands.Update;

public class UpdateSongCommand : IRequest
{
    public SongDto UpdateSong { get; set; }
}

public class UpdateSongCommandHandler : IRequestHandler<UpdateSongCommand>
{
    private readonly ILogger<UpdateSongCommandHandler> _logger;
    private readonly ISongRepository _songRepository;

    public UpdateSongCommandHandler(
        ILogger<UpdateSongCommandHandler> logger,
        ISongRepository songRepository)
    {
        _logger = logger;
        _songRepository = songRepository;
    }

    public async Task Handle(UpdateSongCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var song = await _songRepository.GetSong(request.UpdateSong.Id, cancellationToken);

            if (song == null)
            {
                _logger.LogError("Song not found in db");
                throw new ArgumentNullException(nameof(song));
            }

            if (request.UpdateSong!.Title is not null)
                song.Title = request.UpdateSong!.Title;
            song.Duration = request.UpdateSong!.Duration;
            song.ReleaseDate = request.UpdateSong!.ReleaseDate;

            _songRepository.UpdateSong(song);
            await _songRepository.Save(cancellationToken);
            _logger.LogInformation("Song updated {Id}", song.Id);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "an error occured during updating song");
            throw;
        }
       
    }
}
