using Application.Dto;
using Application.Interfaces.IRepository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Commands.GenreCommands.Update;

public class UpdateGenreCommand : IRequest
{
    public int ArtistId { get; set; }
    public GenreDto Genre { get; set; }
}

public class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand>
{
    private readonly ILogger<UpdateGenreCommandHandler> _logger;
    private readonly IGenreRepository _genreRepository;

    public UpdateGenreCommandHandler(
        ILogger<UpdateGenreCommandHandler> logger,
        IGenreRepository genreRepository)
    {
        _logger = logger;
        _genreRepository = genreRepository;
    }
    public async Task Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var genre = await _genreRepository.GetGenre(request.Genre.Id, cancellationToken);

            if (!_genreRepository.GenreExists(genre.Id))
            {
                _logger.LogError("Genre not found in db");
                throw new ArgumentNullException(nameof(genre));
            }

            if (request.Genre.Name is not null)
                genre.Name = request.Genre.Name;

            _genreRepository.UpdateGenre(request.ArtistId, genre);
            await _genreRepository.Save(cancellationToken);
            _logger.LogInformation("Genre updated: {Id}", genre.Id);

        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "an error occured during updating genre");
            throw;
        }
    }
}
