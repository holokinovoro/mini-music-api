using Application.Dto;
using Application.Interfaces.IRepository;
using MediatR;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;

namespace Application.Features.Queries.Genre;

public class GetAllGenres : IRequest<List<GenreDto>>
{
}

public class GetAllGenresHandle : IRequestHandler<GetAllGenres, List<GenreDto>>
{
    private readonly ILogger<GetAllGenresHandle> _logger;
    private readonly IGenreRepository _genreRepository;

    public GetAllGenresHandle(
        ILogger<GetAllGenresHandle> logger,
        IGenreRepository genreRepository)
    {
        _logger = logger;
        _genreRepository = genreRepository;
    }
    public async Task<List<GenreDto>> Handle(GetAllGenres request, CancellationToken cancellationToken)
    {
        try
        {
            var genres = await _genreRepository.GetGenres(cancellationToken);

            if (genres == null)
                _logger.LogError("genres not found");
            var response = genres.Select(s => new GenreDto
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();

            _logger.LogInformation("Genres taken");
            return response;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "an error occured during getting all genres");
            throw;
        }
    }
}
