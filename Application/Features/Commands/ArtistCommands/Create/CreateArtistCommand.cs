using Application.Dto;
using Application.Interfaces.IRepository;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<CreateArtistCommandHandler> _logger;
        private readonly IArtistRepository _artistRepository;

        public CreateArtistCommandHandler(
            ILogger<CreateArtistCommandHandler> logger,
            IArtistRepository artistRepository)
        {
            _logger = logger;
            _artistRepository = artistRepository;
        }

        public async Task Handle(CreateArtistCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var artist = new Artist
                {
                    Id = request.CreateArtist.Id,
                    Name = request.CreateArtist.Name,
                };

                await _artistRepository.CreateArtist(request.GenreId, artist, cancellationToken);

                _logger.LogInformation("Artist created: {artist}", artist.Name);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred during artist creation");
                throw;
            }
        }
    }
}
