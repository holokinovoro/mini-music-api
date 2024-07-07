using Application.Dto;
using Application.Interfaces.IRepository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Queries.Artist;

public class GetArtistBySongIdQuery : IRequest<ArtistDto>
{
    public int SongId { get; set; }
}

public class GetArtistBySongIdQueryHandler : IRequestHandler<GetArtistBySongIdQuery, ArtistDto>
{
    private readonly ILogger<GetArtistBySongIdQueryHandler> _logger;
    private readonly ISongRepository _songRepository;

    public GetArtistBySongIdQueryHandler(
        ILogger<GetArtistBySongIdQueryHandler> logger,
        ISongRepository songRepository)
    {
        _logger = logger;
        _songRepository = songRepository;
    }
    public async Task<ArtistDto> Handle(GetArtistBySongIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var song = await _songRepository.GetSong(request.SongId, cancellationToken);

            if (song is null)
                _logger.LogError("Song not found");
            var response = new ArtistDto
            {
                Id = song.Artist.Id,
                Name = song.Artist.Name
            };

            _logger.LogInformation("Artist taken {Id}", response.Id);
            return response;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "an error occured during geting artist by song id");
            throw;
        }
    }
}
