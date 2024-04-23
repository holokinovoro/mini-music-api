using Application.Dto;
using Domain.Models;

namespace Application.IRepository
{
    public interface IArtistRepository
    {
        Task<ICollection<Artist>> GetArtists(CancellationToken cancellationToken);

        Task<Artist?> GetArtist(int id, CancellationToken cancellationToken);


        Artist GetArtisyBySong(int songId);

        ICollection<Genre> GetGenreByArtist(int artistId);

       /* Artist GetArtistTrimToUpper(ArtistDto artistCreate);*/

        bool ArtistExists(int id);
        bool ArtistExists(string name);

        Task CreateArtist(int genreId, Artist artist, CancellationToken cancellationToken);

        void UpdateArtist(int genreId, Artist artist);

        bool DeleteArtist(Artist artist);

        Task Save(CancellationToken cancellation);


    }
}
