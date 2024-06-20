using MediatR;
using Application.Dto;
using Application.Interfaces.IRepository;

namespace Application.Features.Queries.Song.GetSong;

public class GetSongsByArtist : IRequest<List<SongDto>>
{
    public int ArtistId { get; set; }
}

public class GetSongsByArtistHandler : IRequestHandler<GetSongsByArtist, List<SongDto>>
{
    private readonly ISongRepository _songRepository;

    public GetSongsByArtistHandler(ISongRepository songRepository)
    {
        _songRepository = songRepository;
    }

    public async Task<List<SongDto>> Handle(GetSongsByArtist request, CancellationToken cancellationToken)
    {
        var songs = await _songRepository.GetSongsByArtist(request.ArtistId, cancellationToken);

        var response = songs.Select(s => new SongDto
        {
            Id = s.Id,
            Title = s.Title,
            Duration = s.Duration,
            ReleaseDate = s.ReleaseDate
        }).ToList();

        return response;
    }
}
