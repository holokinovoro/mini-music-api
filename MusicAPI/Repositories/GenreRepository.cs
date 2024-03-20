using MusicAPI.Data;
using MusicAPI.Dto;
using MusicAPI.Interfaces;
using MusicAPI.Models;

namespace MusicAPI.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly DataContext _context;

        public GenreRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateGenre(int artistId, Genre genre)
        {
            var genreArtistEntity = _context.Artists.Where(e => e.Id == artistId).FirstOrDefault();

            var artistGenre = new ArtistGenre
            {
                Artist = genreArtistEntity,

                Genre = genre
            };

            _context.Add(artistGenre);
            _context.Add(genre);
            return Save();
        }

        public bool GenreExists(int genreId)
        {
            return _context.Genres.Any(p => p.Id == genreId);
        }

        public ICollection<Artist> GetArtistsByGenre(int genreId)
        {
            return _context.ArtistGenres
                .Where(p => p.GenreId == genreId)
                .Select(s => s.Artist)
                .ToList();
        }

        public Genre GetGenre(int genreId)
        {
            return _context.Genres.Where(p => p.Id == genreId).FirstOrDefault();
        }

        public ICollection<Genre> GetGenres()
        {
            return _context.Genres.ToList();
        }

        public Genre GetGenreTrimToUpper(GenreDto genreCreate)
        {
            return GetGenres().Where(c => c.Name.Trim().ToUpper() == genreCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();
        }

        public ICollection<Song> GetSongsByGenre(int genreId)
        {
            return _context.Songs.Where(e => e.Artist.ArtistGenres.Any(ag => ag.GenreId == genreId))
                .ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0 ? true : false;
        }
    }
}
