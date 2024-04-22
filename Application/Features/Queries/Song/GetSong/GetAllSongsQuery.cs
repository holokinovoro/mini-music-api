using MediatR;
using Application.Dto;
using Application.IRepository;

namespace Application.Features.Queries.Song.GetSong
{
    public class GetAllSongsQuery : IRequest<List<SongDto>> { }


    public class GetAllSongsQueryHandler : IRequestHandler<GetAllSongsQuery, List<SongDto>>
    {
        private readonly ISongRepository _songRepository;

        public GetAllSongsQueryHandler(ISongRepository songRepository)
        {
            _songRepository = songRepository;
        }

        public async Task<List<SongDto>> Handle(GetAllSongsQuery request, CancellationToken cancellationToken)
        {
            var songs = await _songRepository.GetSongs(cancellationToken);
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
}

