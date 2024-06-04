using MediatR;
using Application.Dto;
using Domain.Models;
using Application.Interfaces.IRepository;

namespace Application.Features.Commands.SongCommands.Update
{
    public class UpdateSongCommand : IRequest
    {
        public int Id { get; set; }
        public SongDto UpdateSong { get; set; }
    }

    public class UpdateSongCommandHandler : IRequestHandler<UpdateSongCommand>
    {
        private readonly ISongRepository _songRepository;

        public UpdateSongCommandHandler(ISongRepository songRepository)
        {
            _songRepository = songRepository;
        }

        public async Task Handle(UpdateSongCommand request, CancellationToken cancellationToken)
        {
            var song = await _songRepository.GetSong(request.Id, cancellationToken);

            if (song == null)
            {
                throw new ArgumentNullException(nameof(song));
            }

            if (request.UpdateSong!.Title is not null)
                song.Title = request.UpdateSong!.Title;
            song.Duration = request.UpdateSong!.Duration;
            song.ReleaseDate = request.UpdateSong!.ReleaseDate;

            _songRepository.UpdateSong(song);
            await _songRepository.Save(cancellationToken);
           
        }
    }
}
