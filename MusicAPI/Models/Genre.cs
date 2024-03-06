namespace MusicAPI.Models
{
    public class Genre
    {
        public int Id{ get; set; }
        public string Name{ get; set; }

        public ICollection<ArtistGenre> ArtistGenres { get; set; }
    }
}
