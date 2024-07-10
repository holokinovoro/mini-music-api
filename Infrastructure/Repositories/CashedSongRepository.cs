using Application.Interfaces.IRepository;
using Domain.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CashedSongRepository : ISongRepository
    {
        private readonly SongRepository _decorated;
        private readonly IMemoryCache _memoryCashe;

        public CashedSongRepository(
            SongRepository decorated,
            IMemoryCache memoryCashe)
        {
            _decorated = decorated;
            _memoryCashe = memoryCashe;
        }


        public Task<int> CreateSong(Song song, CancellationToken cancellationToken) =>
            _decorated.CreateSong(song, cancellationToken);


        public void DeleteSong(Song song) =>
            _decorated.DeleteSong(song);
        

        public async Task<Song?> GetSong(int songId, CancellationToken cancellationToken)
        {
            string key = $"song-{songId}";

            return  await _memoryCashe.GetOrCreateAsync(
                key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

                    return _decorated.GetSong(songId, cancellationToken);
                }
                );
        }

        public async Task<ICollection<Song>?> GetSongs(CancellationToken cancellationToken)
        {
            string key = "songs";

            return await _memoryCashe.GetOrCreateAsync(
                key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

                    return _decorated.GetSongs(cancellationToken);
                });
        }

        public async Task<ICollection<Song>?> GetSongsByArtist(int artistId, CancellationToken cancellationToken)
        {
            string key = "songs";

            return await _memoryCashe.GetOrCreateAsync(
                key,
                entry =>
                {
                    entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));

                    return _decorated.GetSongsByArtist(artistId, cancellationToken);
                });

        }

        public Task Save(CancellationToken cancellationToken) =>
            _decorated.Save(cancellationToken);


        public bool SongExists(int songId) =>
            _decorated.SongExists(songId);

        public void UpdateSong(Song song) =>
            _decorated.UpdateSong(song);
        
    }
}
