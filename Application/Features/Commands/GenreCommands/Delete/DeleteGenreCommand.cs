using Application.Interfaces.IRepository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Commands.GenreCommands.Delete;

public class DeleteGenreCommand : IRequest
{
    public int GenreId { get; set; }
}

public class DeleteGenreCommandHandler : IRequestHandler<DeleteGenreCommand>
{
    private readonly ILogger<DeleteGenreCommandHandler> _logger;
    private readonly IGenreRepository _genreRepository;

    public DeleteGenreCommandHandler(
        ILogger<DeleteGenreCommandHandler> logger,
        IGenreRepository genreRepository)
    {
        _logger = logger;
        _genreRepository = genreRepository;
    }
    public async Task Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var genre = await _genreRepository.GetGenre(request.GenreId, cancellationToken);

            if (!_genreRepository.GenreExists(genre.Id))
            {
                _logger.LogError("Genre not found in db");
                throw new ArgumentNullException(nameof(genre));
            }
            _genreRepository.DeleteGenre(genre);
            await _genreRepository.Save(cancellationToken);
            _logger.LogInformation("Genre deleted from db {Id}", genre.Id);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "an error occured during deleting genre");
            throw;
        }
    }
}
