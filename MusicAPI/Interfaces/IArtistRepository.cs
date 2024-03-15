using MusicAPI.Dto;
using MusicAPI.Models;

namespace MusicAPI.Interfaces
{
    public interface IArtistRepository
    {
        ICollection<Artist> GetArtists();

        Artist GetArtist(int id);

        ICollection<Song> GetSongsFromArtist(int artistId);

        Artist GetArtisyBySong(int songId);

        ICollection<Genre> GetGenreByArtist(int artistId);

        Artist GetArtistTrimToUpper(ArtistDto artistCreate);

        bool ArtistExists(int id);
        bool ArtistExists(string name);

        bool CreateArtist(int genreId, Artist artist);

        bool Save();


    }
}
