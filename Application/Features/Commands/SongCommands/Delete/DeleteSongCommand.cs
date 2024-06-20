using Application.Interfaces.IRepository;
using MediatR;

namespace Application.Features.Commands.SongCommands.Delete;

public class DeleteSongCommand : IRequest
{
    public int Id { get; set; }
}

public class DeleteSongCommandHandler : IRequestHandler<DeleteSongCommand>
{
    private readonly ISongRepository _songRepository;

    public DeleteSongCommandHandler(ISongRepository songRepository)
    {
        _songRepository = songRepository;
    }

    public async Task Handle(DeleteSongCommand request, CancellationToken cancellationToken)
    {
        var song = await _songRepository.GetSong(request.Id, cancellationToken);

        if (song is not null)
            _songRepository.DeleteSong(song);
        await _songRepository.Save(cancellationToken);
    }
}
