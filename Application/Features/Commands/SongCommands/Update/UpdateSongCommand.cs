using MediatR;
using Application.Dto;
using Application.IRepository;

namespace Application.Features.Commands.SongCommands.Update
{
    public class UpdateSongCommand : IRequest<int>
    {
        public int Id { get; set; }
        public SongDto UpdateSong { get; set; }
    }

   /* public class UpdateSongCommandHandler : IRequestHandler<UpdateSongCommand, int>
    {
        private readonly ISongRepository _songRepository;

        UpdateSongCommandHandler(ISongRepository songRepository)
        {
            _songRepository = songRepository;
        }

        public Task<int> Handle(UpdateSongCommand request, CancellationToken cancellationToken)
        {
            var updateSong = _songRepository.UpdateSong(request);
        }
    }*/
}
