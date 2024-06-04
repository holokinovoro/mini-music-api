using Application.Dto;
using Application.Interfaces.IRepository;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.GenreCommands.Create
{
    public class CreateGenreCommand : IRequest
    {
        public int artistId { get; set; }
        public GenreDto createGenre { get; set; }
    }

    public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand>
    {
        private readonly IGenreRepository _genreRepository;

        public CreateGenreCommandHandler(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }
        public async Task Handle(CreateGenreCommand request, CancellationToken cancellationToken)
        {
            var genre = new Genre
            {
                Id = request.createGenre.Id,
                Name = request.createGenre.Name
            };

            _genreRepository.CreateGenre(request.artistId, genre);
            await _genreRepository.Save(cancellationToken);
        }
    }
}
