using Application.Interfaces.IRepository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Commands.ArtistCommands.Delete;

public class DeleteArtistCommand : IRequest
{
    public int Id { get; set; }
}

public class DeleteArtistCommandHandler : IRequestHandler<DeleteArtistCommand>
{
    private readonly ILogger<DeleteArtistCommandHandler> _logger;
    private readonly IArtistRepository _artistRepository;

    public DeleteArtistCommandHandler(
        ILogger<DeleteArtistCommandHandler> logger,
        IArtistRepository artistRepository)
    {
        _logger = logger;
        _artistRepository = artistRepository;
    }
    public async Task Handle(DeleteArtistCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var artist = await _artistRepository.GetArtist(request.Id, cancellationToken);

            if (!_artistRepository.ArtistExists(artist.Id))
            {
                _logger.LogError("Artist not found in db");
                throw new ArgumentNullException(nameof(artist));
            }

            _artistRepository.DeleteArtist(artist);
            _logger.LogInformation("Artist deleted from db {Id}", artist.Id);
            await _artistRepository.Save(cancellationToken);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "An error occured during deleting Artist");
            throw;
        }
    }
}
