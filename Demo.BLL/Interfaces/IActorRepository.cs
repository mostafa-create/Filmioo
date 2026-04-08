using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IActorRepository : IGenericRepository<Actor>
    {
        public Task<IEnumerable<Actor>> SearchByName(string name);

        public Task<Actor?> GetActorDetailsAsync(int id);

	}
}
