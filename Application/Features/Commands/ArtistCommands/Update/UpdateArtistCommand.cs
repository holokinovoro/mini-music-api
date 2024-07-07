using Application.Dto;
using Application.Interfaces.IRepository;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.ArtistCommands.Update
{
    public class UpdateArtistCommand : IRequest
    {
        public int GenreId { get; set; }

        public ArtistDto ArtistUpdate { get; set; }
    }

    public class UpdateArtistCommandHandler : IRequestHandler<UpdateArtistCommand>
    {
        private readonly ILogger<UpdateArtistCommandHandler> _logger;
        private readonly IArtistRepository _artistRepository;

        public UpdateArtistCommandHandler(
            ILogger<UpdateArtistCommandHandler> logger,
            IArtistRepository artistRepository)
        {
            _logger = logger;
            _artistRepository = artistRepository;
        }
        public async Task Handle(UpdateArtistCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var artist = await _artistRepository.GetArtist(request.ArtistUpdate.Id, cancellationToken);

                if (!_artistRepository.ArtistExists(artist.Id))
                {
                    _logger.LogError("Artist not found in db");
                    throw new ArgumentNullException(nameof(artist));
                }

                if(request.ArtistUpdate.Name is not null)
                {
                    artist.Name = request.ArtistUpdate.Name;
                }

                _artistRepository.UpdateArtist(request.GenreId, artist);
                await _artistRepository.Save(cancellationToken);
                _logger.LogInformation("Artist updated successfully");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occured during updatuin Artist");
                throw;
            }
        }
    }
}
