using Microsoft.EntityFrameworkCore;
using MusicAPI.Models;

namespace MusicAPI.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Artist> Artists { get; set; }

        public DbSet<Song> Songs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<ArtistGenre> ArtistGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ArtistGenre>()
                .HasKey(pk => new
                {
                    pk.ArtistId,
                    pk.GenreId
                });
            modelBuilder.Entity<ArtistGenre>()
                .HasOne(p => p.Artist)
                .WithMany(pc => pc.ArtistGenres)
                .HasForeignKey(fk => fk.ArtistId);
            modelBuilder.Entity<ArtistGenre>()
                .HasOne(p => p.Genre)
                .WithMany(pc => pc.ArtistGenres)
                .HasForeignKey(fk => fk.GenreId);
        }

    }
}
