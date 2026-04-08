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
    public class ReviewRepository : GenericRepository<Review>, IReviewRepository
    {
        private readonly FilmiooDBContext _dbContext;

        public ReviewRepository(FilmiooDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Review>> GetLastReviewsByUserIdAsync(string userId, int count)
        {
            return await _dbContext.Reviews
                .Where(r => r.ApplicationUserId == userId)
                .Include(r => r.Movie)
                .OrderByDescending(r => r.DatePosted) 
                .Take(count)
                .ToListAsync();
        }
    }
}
