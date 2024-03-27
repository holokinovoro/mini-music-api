using MediatR;
using MusicAPI.Interfaces;
using MusicAPI.Models;

namespace MusicAPI.Commands.CreateSong
{
    public class CreateSongCommandHandler : IRequestHandler<CreateSongCommand, int>
    {
        private readonly ISongRepository _songRepository;
        private readonly IArtistRepository _artistRepository;
        /*  private readonly ILogger _logger;*//**/

        public CreateSongCommandHandler(ISongRepository songRepository, IArtistRepository artistRepository)
        {
            _songRepository = songRepository ?? throw new ArgumentNullException(nameof(songRepository));
            _artistRepository = artistRepository ?? throw new ArgumentNullException(nameof(artistRepository));
            /* _logger = logger ?? throw new ArgumentNullException(nameof(logger));*/
        }

        public async Task<int> Handle(CreateSongCommand request, CancellationToken cancellationToken)
        {
            var artist = await _artistRepository.GetArtist(request.artistId, cancellationToken);
            if (artist == null)
            {
                /*_logger.LogWarning("Not found");*/
                return 0;
            }

            var song = new Song
            {
                Id = request.createSong.Id,
                Title = request.createSong.Title,
                Artist = artist,
                Duration = request.createSong.Duration,
                ReleaseDate = request.createSong.ReleaseDate
            };

            await _songRepository.CreateSong(song, cancellationToken);
            /* _logger.LogInformation("Song Created");*/

            return song.Id;
        }
    }
}