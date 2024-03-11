using MusicAPI.Data;
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

        public Artist GetArtist(string name)
        {
            return _context.Artists.Where(p => p.Name == name).FirstOrDefault();
        }

        public ICollection<Song> GetSongsFromArtist(int artistId)
        {
            return _context.Songs.Where(s => s.Artist.Id == artistId).ToList();
        }

        public ICollection<Genre> GetGenreByArtist(int artistId)
        {
            throw new NotImplementedException();
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
    }
}