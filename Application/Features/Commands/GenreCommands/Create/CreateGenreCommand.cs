using Application.Dto;
using Application.Interfaces.IRepository;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Commands.GenreCommands.Create;

public class CreateGenreCommand : IRequest
{
    public int artistId { get; set; }
    public GenreDto createGenre { get; set; }
}

public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand>
{
    private readonly ILogger<CreateGenreCommandHandler> _logger;
    private readonly IGenreRepository _genreRepository;

    public CreateGenreCommandHandler(
        ILogger<CreateGenreCommandHandler> logger,
        IGenreRepository genreRepository)
    {
        _logger = logger;
        _genreRepository = genreRepository;
    }
    public async Task Handle(CreateGenreCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var genre = new Genre
            {
                Id = request.createGenre.Id,
                Name = request.createGenre.Name
            };

            _genreRepository.CreateGenre(request.artistId, genre);
            await _genreRepository.Save(cancellationToken);
            _logger.LogInformation("Genre created: {Id}", genre.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured during creating Genre");
            throw;
        }
    }
}
