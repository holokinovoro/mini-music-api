using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Application.Dto;
using Application.IRepository;
using Domain.Models;

namespace Infrastructure.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly DataContext _context;

        public ArtistRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Artist>> GetArtists(CancellationToken cancellationToken)
        {
            return await _context.Artists.OrderBy(p => p.Id).ToListAsync(cancellationToken);
        }

        public async Task<Artist?> GetArtist(int id, CancellationToken cancellationToken)
        {
            var artist = await _context.Artists.Where(p => p.Id == id).FirstOrDefaultAsync<Artist>(cancellationToken);
            return artist;
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

        public async Task CreateArtist(int genreId, Artist artist, CancellationToken cancellationToken)
        {
            var artistGenreEntity = _context.Genres.Where(e => e.Id == genreId).FirstOrDefault();

            var artistGenre = new ArtistGenre()
            {
                Artist = artist,
                Genre = artistGenreEntity,
            };

            _context.Add(artistGenre);
            _context.Add(artist);
            await Save(cancellationToken);

        }

        public async Task Save(CancellationToken cancellation)
        {
            await _context.SaveChangesAsync();
        }

        public bool UpdateArtist(int genreId, Artist artist)
        {
            throw new NotImplementedException();
        }

        public bool DeleteArtist(Artist artist)
        {
            throw new NotImplementedException();
        }

        /*public Artist GetArtistTrimToUpper(ArtistDto artistCreate)
        {
            return GetArtists().Where(c => c.Name.Trim().ToUpper() == artistCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();
        }*/

        /*public bool UpdateArtist(int genreId, Artist artist)
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
        }*/

        /*public bool DeleteArtist(Artist artist)
        {
            _context.Remove(artist);
            return Save();
        }*/
    }
}