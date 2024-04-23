using Application.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.ArtistCommands.Delete
{
    public class DeleteArtistCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteArtistCommandHandler : IRequestHandler<DeleteArtistCommand>
    {
        private readonly IArtistRepository _artistRepository;

        public DeleteArtistCommandHandler(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }
        public async Task Handle(DeleteArtistCommand request, CancellationToken cancellationToken)
        {
            var artist = await _artistRepository.GetArtist(request.Id, cancellationToken);

            if (!_artistRepository.ArtistExists(artist.Id))
            {
                throw new ArgumentNullException(nameof(artist));
            }

            _artistRepository.DeleteArtist(artist);
            await _artistRepository.Save(cancellationToken);
        }
    }
}
