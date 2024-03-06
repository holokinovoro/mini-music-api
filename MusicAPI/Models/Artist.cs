namespace MusicAPI.Models
{
    public class Artist
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Song> Songs { get; set; }

        public ICollection<ArtistGenre> ArtistGenres { get; set; }
    }
}
