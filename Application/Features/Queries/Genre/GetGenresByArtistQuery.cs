using Application.Dto;
using Application.Interfaces.IRepository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Queries.Genre;

public class GetGenresByArtistQuery : IRequest<List<GenreDto>>
{
    public int artistId { get; set; }
}

public class GetGenresByArtistQueryHandler : IRequestHandler<GetGenresByArtistQuery, List<GenreDto>>
{
    private readonly ILogger<GetGenresByArtistQueryHandler> _logger;
    private readonly IGenreRepository _genreRepository;

    public GetGenresByArtistQueryHandler(
        ILogger<GetGenresByArtistQueryHandler> logger,
        IGenreRepository genreRepository)
    {
        _logger = logger;
        _genreRepository = genreRepository;
    }
    public async Task<List<GenreDto>> Handle(GetGenresByArtistQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var genres = await _genreRepository.GetGenresByArtist(request.artistId, cancellationToken);

            if(genres == null)
            {
                _logger.LogError("Genres not found");
                throw new ArgumentNullException(nameof(genres));
            }
            var response = genres.Select(s => new GenreDto
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();

            _logger.LogInformation("Genres taken");
            return response;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "an error occured during getting all genres");
            throw;
        }
    }
}
