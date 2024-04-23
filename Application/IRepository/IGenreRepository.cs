using Application.Dto;
using Domain.Models;

namespace Application.IRepository
{
    public interface IGenreRepository
    {
        Task<ICollection<Genre>> GetGenres(CancellationToken cancellationToken);

        Task<Genre> GetGenre(int genreId, CancellationToken cancellationToken);

        Task<ICollection<Artist>> GetArtistsByGenre(int genreId, CancellationToken cancellationToken);

        Task<ICollection<Song>> GetSongsByGenre(int genreId, CancellationToken cancellationToken);

        Task<ICollection<Genre>> GetGenresByArtist(int artistId, CancellationToken cancellationToken);

        void CreateGenre(int artistId, Genre genre);

        void UpdateGenre(int artistId, Genre genre);

        void DeleteGenre(Genre genre);

        Task Save(CancellationToken cancellationToken);

        bool GenreExists(int genreId);
    }
}
