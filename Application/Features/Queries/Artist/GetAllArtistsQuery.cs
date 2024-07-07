using Application.Dto;
using Application.Interfaces.IRepository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Queries.Artist;

public class GetAllArtistsQuery : IRequest<List<ArtistDto>>{}

public class GetAllArtistQueryHandler : IRequestHandler<GetAllArtistsQuery, List<ArtistDto>>
{
    private readonly ILogger<GetAllArtistQueryHandler> _logger;
    private readonly IArtistRepository _artistRepository;

    public GetAllArtistQueryHandler(
        ILogger<GetAllArtistQueryHandler> logger,
        IArtistRepository artistRepository)
    {
        _logger = logger;
        _artistRepository = artistRepository;
    }
    public async Task<List<ArtistDto>> Handle(GetAllArtistsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var artists = await _artistRepository.GetArtists(cancellationToken);

            if (artists is null)
                _logger.LogError("Artists not found in db");

            var response = artists.Select(s => new ArtistDto
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();

            _logger.LogInformation("Artists taken from db");
            return response;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "error occured during geting all artists");
            throw;
        }
    }
}
