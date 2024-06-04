using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Application.Dto;
using Domain.Models;
using Application.Interfaces.IRepository;

namespace Infrastructure.Repositories
{
    public class SongRepository : ISongRepository
    {
        private readonly DataContext _context;

        public SongRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<int> CreateSong(Song song, CancellationToken cancellationToken)
        {
            await _context.Songs.AddAsync(song, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void DeleteSong(Song song)
        {
            _context.Remove(song);
        }



        public async Task<Song> GetSong(int songId, CancellationToken cancellationToken)
        {
            var song = await _context.Songs.Where(e => e.Id == songId).FirstOrDefaultAsync(cancellationToken);
            return song;
        }

        public async Task<ICollection<Song>> GetSongs(CancellationToken cancellationToken)
        {
            var songs = _context.Songs.ToListAsync(cancellationToken);
            return await songs;
        }

        public async Task<ICollection<Song>> GetSongsByArtist(int artistId, CancellationToken cancellationToken)
        {
            var songs = await _context.Songs.Where(e => e.Artist.Id == artistId).ToListAsync(cancellationToken);
            return songs;
        }

        public async Task  Save(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public bool SongExists(int songId)
        {
            return _context.Songs.Any(e => e.Id == songId);
        }

        public void UpdateSong(Song song)
        {
            _context.Update(song);
        }
    }
}
