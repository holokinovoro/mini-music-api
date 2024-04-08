using Microsoft.Identity.Client;
using MusicAPI.Models;

namespace MusicAPI.Interfaces
{
    public interface ISongRepository 
    {
        Task<ICollection<Song>> GetSongs(CancellationToken cancellationToken);

        Song GetSong(int songId);

        Artist GetArtistBySong(int songId);

        ICollection<Genre> GetGenreOfSong(int songId);

        bool SongExists(int songId);

        Task<int> CreateSong(Song song, CancellationToken cancellationToken);

        bool UpdateSong(Song song);

        bool DeleteSong(Song song);

        bool Save();
    }
}
