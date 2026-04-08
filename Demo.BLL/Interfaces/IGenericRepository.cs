using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T item);
        void Update(T item);
        void Delete(T item);
        public Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, object>>[]? includes = null);
        public Task<T?> GetByIdAsync(int id);
        public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>[]? includes = null);
        public Task<T?> FindFirstAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>[]? includes = null);

    }
}
