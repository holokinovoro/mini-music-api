using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Application.Dto;
using Application.IRepository;
using Domain.Models;

namespace Infrastructure.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly DataContext _context;

        public GenreRepository(DataContext context)
        {
            _context = context;
        }

        public void CreateGenre(int artistId, Genre genre)
        {
            var genreArtistEntity = _context.Artists.Where(e => e.Id == artistId).FirstOrDefault();

            var artistGenre = new ArtistGenre
            {
                Artist = genreArtistEntity,

                Genre = genre
            };

            _context.Add(artistGenre);
            _context.Add(genre);
        }

        public void DeleteGenre(Genre genre)
        {
            _context.Remove(genre);
        }

        public bool GenreExists(int genreId)
        {
            return _context.Genres.Any(p => p.Id == genreId);
        }

        public async Task<ICollection<Artist>> GetArtistsByGenre(int genreId, CancellationToken cancellationToken)
        {
            return await _context.ArtistGenres
                .Where(p => p.GenreId == genreId)
                .Select(s => s.Artist)
                .ToListAsync();
        }

        public async Task<Genre> GetGenre(int genreId, CancellationToken cancellationToken)
        {
            var genre = await _context.Genres.Where(p => p.Id == genreId).FirstOrDefaultAsync(cancellationToken);
            return genre;
        }

        public async Task<ICollection<Genre>> GetGenres(CancellationToken cancellationToken)
        {
            var genres = await _context.Genres.ToListAsync();
            return genres;
        }

        public async Task<ICollection<Genre>> GetGenresByArtist(int artistId, CancellationToken cancellationToken)
        {
            var genres = await _context.ArtistGenres
                .Where(p => p.ArtistId == artistId)
                .Select(s => s.Genre)
                .ToArrayAsync(cancellationToken);
            return genres;
        }

        public async Task<ICollection<Song>> GetSongsByGenre(int genreId, CancellationToken cancellationToken)
        {
            var songs = await _context.Songs.Where(e => e.Artist.ArtistGenres.Any(ag => ag.GenreId == genreId))
                .ToListAsync(cancellationToken);
            return songs;
        }
       
        public async Task Save(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public void UpdateGenre(int artistId, Genre genre)
        {
            var artistGenreEntity = _context.Artists.Where(e => e.Id == artistId).FirstOrDefault();

            var artistGenre = new ArtistGenre()
            {
                Genre = genre,
                Artist = artistGenreEntity,
            };
            _context.Add(artistGenre);
            _context.Update(genre);
        }
    }
}
