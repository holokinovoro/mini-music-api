using Application.Dto;
using Domain.Models;

namespace Application.IRepository
{
    public interface IGenreRepository
    {
        ICollection<Genre> GetGenres();

        Genre GetGenre(int genreId);

        Task<ICollection<Artist>> GetArtistsByGenre(int genreId, CancellationToken cancellationToken);

        ICollection<Song> GetSongsByGenre(int genreId);

        Genre GetGenreTrimToUpper(GenreDto genreCreate);


        bool CreateGenre(int artistId, Genre genre);

        bool UpdateGenre(int artistId, Genre genre);

        bool DeleteGenre(Genre genre);

        bool Save();

        bool GenreExists(int genreId);
    }
}
