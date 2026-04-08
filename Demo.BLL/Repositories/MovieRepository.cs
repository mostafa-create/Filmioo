using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class MovieRepository: GenericRepository<Movie>,IMovieRepository
    {
        private readonly FilmiooDBContext _dbContext;

        public MovieRepository(FilmiooDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Movie?> GetMovieDetailsAsync(int id)
        {
            return await _dbContext.Movies.Include(m => m.Actors).ThenInclude(ma => ma.Actor).Include(m => m.MovieGenres).ThenInclude(mg => mg.Genre).Include(m => m.Reviews).
                ThenInclude(r => r.ApplicationUser).Include(w => w.WatchList).FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<Movie> GetMovieWithActorsAsync(int id)
        {
            var movie = await _dbContext.Movies
                .Include(m => m.Actors) 
                .ThenInclude(ma => ma.Actor) 
                .FirstOrDefaultAsync(m => m.Id == id);
            return movie!;
        }
        public async Task<decimal> GetAverageRating(int movieId)
        {
            decimal? averageRating = await _dbContext.Reviews
                .Where(r => r.MovieId == movieId)
                .AverageAsync(r => (decimal?)r.Score);

            return averageRating ?? 0m;
        }
		public async Task<IEnumerable<Movie>> SearchByName(string name)
		{
			return await _dbContext.Movies
				.Where(m => m.Title.Contains(name))
				.ToListAsync();
		}
		public async Task<List<Movie>> GetTrending(int count = 10)
        {
            var lastWeek = DateTime.Now.AddDays(-7);

            return await _dbContext.Movies
                .OrderByDescending(m => m.Reviews.Count(r => r.DatePosted >= lastWeek))
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<Movie>> GetRecentlyAdded(int count = 10)
        {
            return await _dbContext.Movies
                .OrderByDescending(m => m.DateAddedToDB)
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<Movie>> GetTopRated(int count = 10)
        {
            return await _dbContext.Movies
                .OrderByDescending(m => m.Rating)
                .Take(count)
                .ToListAsync();
        }
        public async Task UpdateMovieActorsAsync(int movieId, List<int> actorIds)
        {
            var existingLinks = await _dbContext.MovieActors
                .Where(ma => ma.MovieId == movieId).ToListAsync();

            _dbContext.MovieActors.RemoveRange(existingLinks);

            var newLinks = actorIds.Select(aId => new MovieActor
            {
                MovieId = movieId,
                ActorId = aId
            });

            await _dbContext.MovieActors.AddRangeAsync(newLinks);
        }
        public async Task UpdateMovieGenresAsync(int movieId, List<int> GenreIds)
        {
            var existingLinks = await _dbContext.MovieGenres
                .Where(ma => ma.MovieId == movieId).ToListAsync();

            _dbContext.MovieGenres.RemoveRange(existingLinks);

            var newLinks = GenreIds.Select(aId => new MovieGenre
            {
                MovieId = movieId,
                GenreId = aId
            });

            await _dbContext.MovieGenres.AddRangeAsync(newLinks);
        }



    }
}
