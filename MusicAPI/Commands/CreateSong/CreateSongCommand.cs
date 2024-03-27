using MediatR;
using MusicAPI.Dto;
using MusicAPI.Models;

namespace MusicAPI.Commands.CreateSong
{
    public class CreateSongCommand : IRequest<int>
    {
        public int artistId { get; set; }
        public SongDto createSong { get; set; }
    }
}
