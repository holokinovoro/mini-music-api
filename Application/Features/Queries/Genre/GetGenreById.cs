using Application.Dto;
using Application.Interfaces.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.Genre
{
    public class GetGenreById : IRequest<GenreDto>
    {
        public int Id { get; set; }
    }

    public class GetGenreByIdHandler : IRequestHandler<GetGenreById, GenreDto>
    {
        private readonly IGenreRepository _genreRepository;

        public GetGenreByIdHandler(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }
        public async Task<GenreDto> Handle(GetGenreById request, CancellationToken cancellationToken)
        {
            var genre = await _genreRepository.GetGenre(request.Id, cancellationToken);

            if (!_genreRepository.GenreExists(genre.Id))
                throw new ArgumentNullException(nameof(genre));
            var response = new GenreDto
            {
                Id = genre.Id,
                Name = genre.Name
            };
            return response;
        }
    }
}
