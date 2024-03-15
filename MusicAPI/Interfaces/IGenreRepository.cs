using MusicAPI.Models;

namespace MusicAPI.Interfaces
{
    public interface IGenreRepository
    {
        ICollection<Genre> GetGenres();

        Genre GetGenre(int genreId);

        ICollection<Artist> GetArtistsByGenre(int genreId);

        ICollection<Song> GetSongsByGenre(int genreId);

        bool GenreExists(int genreId);
    }
}
