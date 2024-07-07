using Application.Dto;
using Application.Interfaces.IRepository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Queries.Artist;

public class GetArtistByIdQuery : IRequest<ArtistDto>
{
    public int ArtistId { get; set; }
}

public class GetArtistByIdQueryHandler : IRequestHandler<GetArtistByIdQuery, ArtistDto>
{
    private readonly ILogger<GetArtistByIdQueryHandler> _logger;
    private readonly IArtistRepository _artistRepository;

    public GetArtistByIdQueryHandler(
        ILogger<GetArtistByIdQueryHandler> logger,
        IArtistRepository artistRepository)
    {
        _logger = logger;
        _artistRepository = artistRepository;
    }
    public async Task<ArtistDto> Handle(GetArtistByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var artist = await _artistRepository.GetArtist(request.ArtistId, cancellationToken);

            if (artist is null)
                _logger.LogInformation("Artist not found");
            var response = new ArtistDto
            {
                Id = artist.Id,
                Name = artist.Name
            };

            _logger.LogInformation("Artist taken {Id}", artist.Id);
            return response;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "error occured during geting artist");
            throw;
        }
    }
}
