using Application.Dto;
using Application.Interfaces.IRepository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Queries.Genre;

public class GetGenreById : IRequest<GenreDto>
{
    public int Id { get; set; }
}

public class GetGenreByIdHandler : IRequestHandler<GetGenreById, GenreDto>
{
    private readonly ILogger<GetGenreByIdHandler> _logger;
    private readonly IGenreRepository _genreRepository;

    public GetGenreByIdHandler(
        ILogger<GetGenreByIdHandler> logger,
        IGenreRepository genreRepository)
    {
        _logger = logger;
        _genreRepository = genreRepository;
    }
    public async Task<GenreDto> Handle(GetGenreById request, CancellationToken cancellationToken)
    {
        try
        {
            var genre = await _genreRepository.GetGenre(request.Id, cancellationToken);

            if (!_genreRepository.GenreExists(genre.Id))
            {
                _logger.LogError("Genre not found");
                throw new ArgumentNullException(nameof(genre));
            }
            var response = new GenreDto
            {
                Id = genre.Id,
                Name = genre.Name
            };

            _logger.LogInformation("Genre taken {Id}", genre.Id);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "an error occured during getting all genres");
            throw;
        }
    }
}
