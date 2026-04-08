using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly FilmiooDBContext _dbContext;
        public GenericRepository(FilmiooDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, object>>[]? includes = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.ToListAsync();
        }
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>[]? includes = null)
        {
            IQueryable<T> query = _dbContext.Set<T>().Where(predicate);

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.ToListAsync();
        }
        public async Task<T?> FindFirstAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>[]? includes = null)
        {
            IQueryable<T> query = _dbContext.Set<T>().Where(predicate);

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task AddAsync(T item)
        {
            await _dbContext.AddAsync(item);
        }

        public void Delete(T item)
        {
            _dbContext.Remove(item);
        }

        public void Update(T item)
        {
            _dbContext.Update(item);
        }
    }
}
