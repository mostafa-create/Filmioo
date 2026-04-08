using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly FilmiooDBContext dbContext;

        public IMovieRepository MovieRepository { get; set; }
        public IActorRepository ActorRepository { get; set; }
        public IGenreRepository GenreRepository { get; set; }
        public IReviewRepository ReviewRepository { get; set; }
        public IWatchListRepository WatchListRepository { get; set; }

        public UnitOfWork(FilmiooDBContext dbContext)
        {
            MovieRepository = new MovieRepository(dbContext);
            ActorRepository = new ActorRepository(dbContext);
            GenreRepository = new GenreRepository(dbContext);
            ReviewRepository = new ReviewRepository(dbContext);
            WatchListRepository = new WatchListRepository(dbContext);
            this.dbContext = dbContext;
        }
        public async Task<int> Complete()
        {
            return await dbContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
