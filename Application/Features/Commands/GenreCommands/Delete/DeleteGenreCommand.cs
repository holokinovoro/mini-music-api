using Application.Interfaces.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.GenreCommands.Delete
{
    public class DeleteGenreCommand : IRequest
    {
        public int GenreId { get; set; }
    }

    public class DeleteGenreCommandHandler : IRequestHandler<DeleteGenreCommand>
    {
        private readonly IGenreRepository _genreRepository;

        public DeleteGenreCommandHandler(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }
        public async Task Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = await _genreRepository.GetGenre(request.GenreId, cancellationToken);

            if (!_genreRepository.GenreExists(genre.Id))
                _genreRepository.DeleteGenre(genre);
            await _genreRepository.Save(cancellationToken);
        }
    }
}
