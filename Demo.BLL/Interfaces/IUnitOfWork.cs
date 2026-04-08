using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IUnitOfWork
    {
        public IMovieRepository MovieRepository { get; set; }
        public IActorRepository ActorRepository { get; set; }
        public IGenreRepository GenreRepository { get; set; }
        public IReviewRepository ReviewRepository { get; set; }
        public IWatchListRepository WatchListRepository { get; set; }
        Task<int> Complete();
    }
}
