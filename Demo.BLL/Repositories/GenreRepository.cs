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
    public class GenreRepository : GenericRepository<Genre>,IGenreRepository
    {
        private readonly FilmiooDBContext _dbContext;

        public GenreRepository(FilmiooDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Genre> GetGenreWithMoviesAsync(int id)
        {
            return await _dbContext.Genres
                .Include(g => g.Movies)    
                .ThenInclude(mg => mg.Movie)
                .FirstOrDefaultAsync(g => g.GenreId == id);
        }
    }
}
