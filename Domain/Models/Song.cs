using System.Reflection.Metadata;

namespace Domain.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public Artist Artist { get; set; }

        public string Duration { get; set; }

        public DateTime ReleaseDate { get; set; }


    }
}
