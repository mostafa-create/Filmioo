using Demo.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Contexts
{
    public class FilmiooDBContext : IdentityDbContext<ApplicationUser>
    {
        public FilmiooDBContext(DbContextOptions<FilmiooDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<MovieGenre>()
           .HasKey(mp => new { mp.MovieId, mp.GenreId });
            builder.Entity<MovieActor>()
           .HasKey(mp => new { mp.MovieId, mp.ActorId });
            builder.Entity<WatchListItem>()
            .HasKey(w => new { w.MovieId, w.UserId });
            base.OnModelCreating(builder);

            builder.Entity<Review>()
            .Property(r => r.Score)
            .HasPrecision(3, 1); 
        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<WatchListItem> WatchListItems { get; set; }


    }
}
