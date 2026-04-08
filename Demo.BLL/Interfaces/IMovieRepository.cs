using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IMovieRepository: IGenericRepository<Movie>
    {
        Task<decimal> GetAverageRating(int movieId);
        //var filteredMovies = await movieRepository.Find(
        //movie => movie.ReleaseYear > 2020 && movie.Rating == "PG-13"
        //);
        public Task<Movie?> GetMovieDetailsAsync(int id);
        public Task<IEnumerable<Movie>> SearchByName(string name);

		public Task<List<Movie>> GetTopRated(int count);
        public Task<List<Movie>> GetRecentlyAdded(int count);
        public Task<List<Movie>> GetTrending(int count);
        public Task UpdateMovieActorsAsync(int movieId, List<int> actorIds);
        public Task UpdateMovieGenresAsync(int movieId, List<int> GenreIds);
        public Task<Movie> GetMovieWithActorsAsync(int id);

    }
}
