using Domain.Models;

namespace Application.Dto
{
    public class SongDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Duration { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}
