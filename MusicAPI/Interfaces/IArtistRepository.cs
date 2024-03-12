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

        bool ArtistExists(int id);
        bool ArtistExists(string name);

        void CreateArtist(Artist artist);


    }
}
