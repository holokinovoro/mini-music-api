using MusicAPI.Dto;
using MusicAPI.Models;

namespace MusicAPI.Interfaces
{
    public interface IArtistRepository
    {
        Task<ICollection<Artist>> GetArtists();

        Task<Artist?> GetArtist(int id, CancellationToken cancellationToken);

        ICollection<Song> GetSongsFromArtist(int artistId);

        Artist GetArtisyBySong(int songId);

        ICollection<Genre> GetGenreByArtist(int artistId);

       /* Artist GetArtistTrimToUpper(ArtistDto artistCreate);*/

        bool ArtistExists(int id);
        bool ArtistExists(string name);

        bool CreateArtist(int genreId, Artist artist);

        bool UpdateArtist(int genreId, Artist artist);

        bool DeleteArtist(Artist artist);

        bool Save();


    }
}
