using MusicAPI.Models;

namespace MusicAPI.Interfaces
{
    public interface IArtistRepository
    {
        ICollection<Artist> GetArtists();

        Artist GetArtist(int id);
        Artist GetArtist(string name);

        ICollection<Song> GetSongsFromArtist(int artistId);

        ICollection<Genre> GetGenreByArtist(int artistId);

        bool ArtistExists(int id);
        bool ArtistExists(string name);

        void CreateArtist(Artist artist);


    }
}
