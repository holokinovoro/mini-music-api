using Application.Dto;
using Application.Interfaces.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.Artist
{
    public class GetArtistBySongIdQuery : IRequest<ArtistDto>
    {
        public int SongId { get; set; }
    }

    public class GetArtistBySongIdQueryHandler : IRequestHandler<GetArtistBySongIdQuery, ArtistDto>
    {
        private readonly ISongRepository _songRepository;

        public GetArtistBySongIdQueryHandler(ISongRepository songRepository)
        {
            _songRepository = songRepository;
        }
        public async Task<ArtistDto> Handle(GetArtistBySongIdQuery request, CancellationToken cancellationToken)
        {
            var song = await _songRepository.GetSong(request.SongId, cancellationToken);

            var response = new ArtistDto
            {
                Id = song.Artist.Id,
                Name = song.Artist.Name
            };

            return response;
        }
    }
}
