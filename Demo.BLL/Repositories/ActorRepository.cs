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
    public class ActorRepository : GenericRepository<Actor>, IActorRepository
    {
        private readonly FilmiooDBContext _dbContext;

        public ActorRepository(FilmiooDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Actor>> SearchByName(string name)
        {
            return await _dbContext.Actors
                .Where(m => m.FirstName.Contains(name) || m.LastName.Contains(name)) 
                .ToListAsync();
        }
        public async Task<Actor?> GetActorDetailsAsync(int id)
		{
			return await _dbContext.Actors.Include(a => a.Movies).ThenInclude(ma => ma.Movie).FirstOrDefaultAsync(a => a.Id == id);
		}
        
	}
}
