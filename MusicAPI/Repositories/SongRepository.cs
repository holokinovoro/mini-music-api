using MusicAPI.Data;
using MusicAPI.Interfaces;
using MusicAPI.Models;

namespace MusicAPI.Repositories
{
    public class SongRepository : ISongRepository
    {
        private readonly DataContext _context;

        public SongRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateSong(Song song)
        {
            _context.Add(song);
            return Save();
        }

        public Artist GetArtistBySong(int songId)
        {
            return _context.Songs.Where(e => e.Id == songId).Select(p => p.Artist).FirstOrDefault();
        }

        public ICollection<Genre> GetGenreOfSong(int songId)
        {
            return _context.Songs.Where(e => e.Id == songId)
                .SelectMany(s => s.Artist.ArtistGenres.Select(ag => ag.Genre))
                .Distinct().ToList();
        }

        public Song GetSong(int songId)
        {
            return _context.Songs.Where(e => e.Id == songId).FirstOrDefault();
        }

        public ICollection<Song> GetSongs()
        {
            return _context.Songs.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool SongExists(int songId)
        {
            return _context.Songs.Any(e => e.Id == songId);
        }
    }
}
