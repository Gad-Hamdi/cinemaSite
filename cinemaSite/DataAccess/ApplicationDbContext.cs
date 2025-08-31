using CinemaTask.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaTask.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<ActorMovie> ActorMovies { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ActorMovie>()
               .HasKey(am => new { am.ActorId, am.MovieId });

            // Configure the many-to-many relationship through the junction table
            modelBuilder.Entity<ActorMovie>()
                .HasOne(am => am.Actor)
                .WithMany()  // ✅ No navigation property to ActorMovie in Actor class
                .HasForeignKey(am => am.ActorId);

            modelBuilder.Entity<ActorMovie>()
                .HasOne(am => am.Movie)
                .WithMany()  
                .HasForeignKey(am => am.MovieId);

            // Configure Movie relationships
            modelBuilder.Entity<Movie>()
                .HasOne(m => m.Category)
                .WithMany(c => c.Movies)
                .HasForeignKey(m => m.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Movie>()
                .HasOne(m => m.Cinema)
                .WithMany(c => c.Movies)
                .HasForeignKey(m => m.CinemaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Set default values for string properties
            modelBuilder.Entity<Movie>()
                .Property(m => m.Name)
                .HasDefaultValue("Unknown Movie");

            modelBuilder.Entity<Actor>()
                .Property(a => a.FirstName)
                .HasDefaultValue("Unknown");

            modelBuilder.Entity<Actor>()
                .Property(a => a.LastName)
                .HasDefaultValue("Actor");



        }
    }
}