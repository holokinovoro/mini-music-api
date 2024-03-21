using MusicAPI.Data;
using MusicAPI.Dto;
using MusicAPI.Interfaces;
using MusicAPI.Models;

namespace MusicAPI.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly DataContext _context;

        public ArtistRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Artist> GetArtists()
        {
            return _context.Artists.OrderBy(p => p.Id).ToList();
        }

        public Artist GetArtist(int id)
        {
            return _context.Artists.Where(p => p.Id == id).FirstOrDefault();
        }
       

        public ICollection<Song> GetSongsFromArtist(int artistId)
        {
            return _context.Songs.Where(s => s.Artist.Id == artistId).ToList();
        }

        public ICollection<Genre> GetGenreByArtist(int artistId)
        {
            return _context.ArtistGenres
                .Where(e => e.ArtistId == artistId)
                .Select(c => c.Genre).ToList();
        }

        public bool ArtistExists(int id)
        {
            return _context.Artists.Any(p => p.Id == id);
        }

        public bool ArtistExists(string name)
        {
            return _context.Artists.Any(p => p.Name == name);
        }

        public void CreateArtist(Artist artist)
        {
            _context.Artists.Add(artist);
            _context.SaveChanges();
        }

        public Artist GetArtisyBySong(int songId)
        {
            return _context.Songs.Where(e => e.Id == songId).Select(c => c.Artist).FirstOrDefault();
        }

        public bool CreateArtist(int genreId, Artist artist)
        {
            var artistGenreEntity = _context.Genres.Where(e => e.Id == genreId).FirstOrDefault();

            var artistGenre = new ArtistGenre()
            {
                Artist = artist,
                Genre = artistGenreEntity,
            };

            _context.Add(artistGenre);
            _context.Add(artist);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public Artist GetArtistTrimToUpper(ArtistDto artistCreate)
        {
            return GetArtists().Where(c => c.Name.Trim().ToUpper() == artistCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();
        }

        public bool UpdateArtist(int genreId, Artist artist)
        {
            var artistGenreEntity = _context.Genres.Where(e => e.Id == genreId).FirstOrDefault();

            var artistGenre = new ArtistGenre()
            {
                Artist = artist,
                Genre = artistGenreEntity,
            };
            _context.Add(artistGenre);
            _context.Update(artist);
            return Save();
        }

        public bool DeleteArtist(Artist artist)
        {
            _context.Remove(artist);
            return Save();
        }
    }
}