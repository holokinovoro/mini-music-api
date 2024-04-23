using Application.Dto;
using Application.IRepository;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.ArtistCommands.Create
{
    public class CreateArtistCommand : IRequest
    {
        public ArtistDto CreateArtist { get; set; }
        public int GenreId { get; set; }
    }

    public class CreateArtistCommandHandler : IRequestHandler<CreateArtistCommand>
    {
        private readonly IArtistRepository _artistRepository;

        public CreateArtistCommandHandler(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public async Task Handle(CreateArtistCommand request, CancellationToken cancellationToken)
        {
            var artist = new Artist
            {
                Id = request.CreateArtist.Id,
                Name = request.CreateArtist.Name,
            };

            await _artistRepository.CreateArtist(request.GenreId, artist, cancellationToken);

        }
    }
}
