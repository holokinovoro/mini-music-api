using Application.Dto;
using Application.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.Artist
{
    public class GetArtistsByGenre : IRequest<List<ArtistDto>>
    {
        public int GenreId { get; set; }
    }

    public class GetArtistsByGenreHandler : IRequestHandler<GetArtistsByGenre, List<ArtistDto>>
    {
        private readonly IGenreRepository _genreRepository;

        public GetArtistsByGenreHandler(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }
        public async Task<List<ArtistDto>> Handle(GetArtistsByGenre request, CancellationToken cancellationToken)
        {
            var artist = await _genreRepository.GetArtistsByGenre(request.GenreId, cancellationToken);

            var response = artist.Select(s => new ArtistDto
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();

            return response;
        }
    }
}
