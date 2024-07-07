using Application.Dto;
using Application.Interfaces.IRepository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Queries.Artist;

public class GetArtistsByGenre : IRequest<List<ArtistDto>>
{
    public int GenreId { get; set; }
}

public class GetArtistsByGenreHandler : IRequestHandler<GetArtistsByGenre, List<ArtistDto>>
{
    private readonly ILogger<GetArtistsByGenreHandler> _logger;
    private readonly IGenreRepository _genreRepository;

    public GetArtistsByGenreHandler(
        ILogger<GetArtistsByGenreHandler> logger,
        IGenreRepository genreRepository)
    {
        _logger = logger;
        _genreRepository = genreRepository;
    }
    public async Task<List<ArtistDto>> Handle(GetArtistsByGenre request, CancellationToken cancellationToken)
    {
        try
        {
            var artist = await _genreRepository.GetArtistsByGenre(request.GenreId, cancellationToken);

            if (artist is null)
                _logger.LogError("artist not found");
            var response = artist.Select(s => new ArtistDto
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();

            _logger.LogInformation("Artists taken");
            return response;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "an error occured during geting artists by genre id");
            throw;
        }
    }
}
