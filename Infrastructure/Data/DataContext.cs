using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Microsoft.Extensions.Options;
using Infrastructure.Configurations;

namespace Infrastructure.Data
{
    public class DataContext( DbContextOptions<DataContext> options,
    IOptions<AuthorizationOptions> authOptions) : DbContext(options)
    { 

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<Artist> Artists { get; set; }

        public DbSet<Song> Songs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<ArtistGenre> ArtistGenres { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }


        public DbSet<PermissionEntity> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }


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

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
            modelBuilder.ApplyConfiguration(new RolePermissionConfiguration(authOptions.Value));
        }

    }
}
