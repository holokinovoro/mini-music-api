using MusicAPI.Models;

namespace MusicAPI.Dto
{
    public class SongDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int Duration { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}
