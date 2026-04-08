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
    public class WatchListRepository : GenericRepository<WatchListItem>, IWatchListRepository
    {
        private readonly FilmiooDBContext _dbContext;

        public WatchListRepository(FilmiooDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<WatchListItem>> GetWatchListByUserIdAsync(string userId)
        {
            return await _dbContext.WatchListItems
                .Where(w => w.UserId == userId)
                .Include(w => w.Movie) 
                .ToListAsync();
        }
    }
}
