using Application.Dto;
using Application.Interfaces.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.ArtistCommands.Update
{
    public class UpdateArtistCommand : IRequest
    {
        public int ArtistId { get; set; }
        public int GenreId { get; set; }

        public ArtistDto ArtistUpdate { get; set; }
    }

    public class UpdateArtistCommandHandler : IRequestHandler<UpdateArtistCommand>
    {
        private readonly IArtistRepository _artistRepository;

        public UpdateArtistCommandHandler(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }
        public async Task Handle(UpdateArtistCommand request, CancellationToken cancellationToken)
        {
            var artist = await _artistRepository.GetArtist(request.ArtistId, cancellationToken);

            if(!_artistRepository.ArtistExists(artist.Id))
                throw new ArgumentNullException(nameof(artist));

            if(request.ArtistUpdate.Name is not null)
            {
                artist.Name = request.ArtistUpdate.Name;
            }

            _artistRepository.UpdateArtist(request.GenreId, artist);
            await _artistRepository.Save(cancellationToken);
        }
    }
}
