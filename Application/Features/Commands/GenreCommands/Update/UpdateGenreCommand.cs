using Application.Dto;
using Application.Interfaces.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.GenreCommands.Update
{
    public class UpdateGenreCommand : IRequest
    {
        public int GenreId { get; set; }
        public int ArtistId { get; set; }
        public GenreDto Genre { get; set; }
    }

    public class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand>
    {
        private readonly IGenreRepository _genreRepository;

        public UpdateGenreCommandHandler(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }
        public async Task Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = await _genreRepository.GetGenre(request.GenreId, cancellationToken);

            if (!_genreRepository.GenreExists(genre.Id))
                throw new ArgumentNullException(nameof(genre));

            if (request.Genre.Name is not null)
                genre.Name = request.Genre.Name;

            _genreRepository.UpdateGenre(request.ArtistId, genre);
            await _genreRepository.Save(cancellationToken);
        }
    }
}
