using MusicAPI.Dto;
using MusicAPI.Models;

namespace MusicAPI.Interfaces
{
    public interface IGenreRepository
    {
        ICollection<Genre> GetGenres();

        Genre GetGenre(int genreId);

        ICollection<Artist> GetArtistsByGenre(int genreId);

        ICollection<Song> GetSongsByGenre(int genreId);

        Genre GetGenreTrimToUpper(GenreDto genreCreate);


        bool CreateGenre(int artistId, Genre genre);

        bool UpdateGenre(int artistId, Genre genre);

        bool Save();

        bool GenreExists(int genreId);
    }
}
