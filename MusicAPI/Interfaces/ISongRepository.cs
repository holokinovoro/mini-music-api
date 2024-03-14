using Microsoft.Identity.Client;
using MusicAPI.Models;

namespace MusicAPI.Interfaces
{
    public interface ISongRepository
    {
        ICollection<Song> GetSongs();

        Song GetSong(int songId);

        Artist GetArtistBySong(int songId);

        ICollection<Genre> GetGenreOfSong(int songId);

        bool SongExists(int songId);
    }
}
