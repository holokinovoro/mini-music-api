using Domain.Models;

namespace Application.Interfaces.IRepository;

public interface ISongRepository
{
    Task<ICollection<Song>> GetSongs(CancellationToken cancellationToken);

    Task<Song> GetSong(int songId, CancellationToken cancellationToken);


    Task<ICollection<Song>> GetSongsByArtist(int artistId, CancellationToken cancellationToken);


    bool SongExists(int songId);

    Task<int> CreateSong(Song song, CancellationToken cancellationToken);

    void UpdateSong(Song song);

    void DeleteSong(Song song);

    Task Save(CancellationToken cancellationToken);
}
