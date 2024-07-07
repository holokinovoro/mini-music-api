using Application.Interfaces.IRepository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Commands.SongCommands.Delete;

public class DeleteSongCommand : IRequest
{
    public int Id { get; set; }
}

public class DeleteSongCommandHandler : IRequestHandler<DeleteSongCommand>
{
    private readonly ILogger<DeleteSongCommandHandler> _logger;
    private readonly ISongRepository _songRepository;

    public DeleteSongCommandHandler(
        ILogger<DeleteSongCommandHandler> logger,
        ISongRepository songRepository)
    {
        _logger = logger;
        _songRepository = songRepository;
    }

    public async Task Handle(DeleteSongCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var song = await _songRepository.GetSong(request.Id, cancellationToken);

            if (song is not null)
                _songRepository.DeleteSong(song);
            await _songRepository.Save(cancellationToken);
            _logger.LogInformation("Song deleted {Id}", song.Id);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "an error occured during deleting song");
            throw;
        }
    }
}
