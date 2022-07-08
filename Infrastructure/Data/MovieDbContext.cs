using ApplicationCore.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class MovieShopDbContext : DbContext

    {
        public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options): base(options)
        {

        }

        public DbSet <Genre> Genres { get; set; }
        public DbSet <Movie> Movies { get; set; }
        public DbSet <MovieGenre> MoviesGenres { get; set; }
        public DbSet <Cast> Casts { get; set; }
        public DbSet <MovieCast> MoviesCasts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Trailer> Trailers { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Crew> Crewers { get; set; }
        public DbSet<MovieCrew> MovieCrews { get; set; }
        public DbSet<User> Users { get; set; }
        


        //virtual method

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(ConfigureMoive);
            modelBuilder.Entity<MovieGenre>(ConfigureMovieGenre);
            modelBuilder.Entity<MovieCast>(ConfigureMovieCast);
            modelBuilder.Entity<UserRole>(ConfigureUserRole);
            modelBuilder.Entity<Review>(ConfigureReview);
            modelBuilder.Entity<Favorite>(ConfigureFavorite);
            modelBuilder.Entity<Purchase>(ConfigurePurchase);
            modelBuilder.Entity<MovieCrew>(ConfigureMovieCrew);
            modelBuilder.Entity<User>(ConfigureUser);
        }

        private void ConfigureUser(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName).HasMaxLength(128).IsRequired(false);
            builder.Property(x => x.LastName).HasMaxLength(128).IsRequired(false);
            builder.Property(x => x.Email).HasMaxLength(256).IsRequired(false);
            builder.Property(x => x.HashedPassword).HasMaxLength(1024).IsRequired(false);
            builder.Property(x => x.Salt).HasMaxLength(1024).IsRequired(false);
            builder.Property(x => x.PhoneNumber).HasMaxLength(16).IsRequired(false);
            builder.Property(x => x.TwoFactorEnabled).IsRequired(false);
            builder.Property(x => x.IsLocked).IsRequired(false);
            builder.Property(m => m.DateOfBirth).HasDefaultValueSql("getdate()").IsRequired(false);
            builder.Property(m => m.lockoutEndDate).HasDefaultValueSql("getdate()").IsRequired(false);
            builder.Property(m => m.LastLoginDateTime).HasDefaultValueSql("getdate()").IsRequired(false);
            builder.Property(x => x.AccessFailedCount).IsRequired(false);
        }

        private void ConfigureMovieCrew(EntityTypeBuilder<MovieCrew> builder)
        {
            builder.ToTable("MovieCrew");
            builder.HasKey(x => new { x.MovieId, x.CrewId});
            builder.Property(x => x.Job).HasMaxLength(128);
            builder.Property(x => x.Department).HasMaxLength(128);
        }

        private void ConfigurePurchase(EntityTypeBuilder<Purchase> builder)
        {
            builder.ToTable("Purchase");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.TotalPrice).HasColumnType("decimal(18,2)");
            builder.Property(m => m.PurchaseDateTime).HasDefaultValueSql("getdate()");
        }

        private void ConfigureFavorite(EntityTypeBuilder<Favorite> builder)
        {
            builder.ToTable("Favorite");
            builder.HasKey(x => x.Id);
        }

        private void ConfigureReview(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Review");
            builder.HasKey(x => new { x.MovieId, x.UserId });
            builder.Property(x => x.Rating).HasColumnType("decimal(3, 2)");
        }

        private void ConfigureUserRole(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRole");
            builder.HasKey(x => new { x.UserId, x.RoleId });
        }

        
        private void ConfigureMovieCast(EntityTypeBuilder<MovieCast> builder)
        {
            builder.ToTable("MovieCast");
            builder.HasKey(x => new { x.MovieId, x.CastId, x.Character });
            builder.Property(x => x.Character).HasMaxLength(450);
        }
        private void ConfigureMovieGenre(EntityTypeBuilder<MovieGenre> builder)
        {
            builder.ToTable("MovieGenre");
            builder.HasKey(x => new { x.MovieId, x.GenreId });
        }
        private void ConfigureMoive(EntityTypeBuilder<Movie> builder)
        {
            builder.ToTable("Movie");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasMaxLength(256);
            builder.Property(m => m.Overview).HasMaxLength(4096);
            builder.Property(m => m.Tagline).HasMaxLength(512);
            builder.Property(m => m.ImdbUrl).HasMaxLength(2084);
            builder.Property(m => m.TmdbUrl).HasMaxLength(2084);
            builder.Property(m => m.PosterUrl).HasMaxLength(2084);
            builder.Property(m => m.BackdropUrl).HasMaxLength(2084);
            builder.Property(m => m.OriginalLanguage).HasMaxLength(64);
            builder.Property(m => m.ReleaseDate).HasDefaultValueSql("getdate()");
            builder.Property(m => m.Price).HasColumnType("decimal(5, 2)").HasDefaultValue(9.9m);
            builder.Property(m => m.Budget).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);
            builder.Property(m => m.Revenue).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);
            builder.Property(m => m.CreatedDate).HasDefaultValueSql("getdate()");
            builder.Property(m => m.UpdatedDate).HasDefaultValueSql("getdate()");
        }
    }
}
