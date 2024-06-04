using MediatR;
using Application.Dto;
using Domain.Models;
using Application.Interfaces.IRepository;

namespace Application.Features.Queries.Song.GetSong
{
    public class GetSongByIdQuery : IRequest<SongDto>
    {
        public int SongId { get; set; }
    }

    public class GetSongByIdQueryHandler : IRequestHandler<GetSongByIdQuery, SongDto>
    {
        private readonly ISongRepository _songRepository;

        public GetSongByIdQueryHandler(ISongRepository songRepository)
        {
            _songRepository = songRepository;
        }

        public async Task<SongDto> Handle(GetSongByIdQuery request, CancellationToken cancellationToken)
        {
            var song = await _songRepository.GetSong(request.SongId, cancellationToken);
            var response = new SongDto
            {
                Id = song.Id,
                Title = song.Title,
                Duration = song.Duration,
                ReleaseDate = song.ReleaseDate
            };
            return response;

        }
    }
}
