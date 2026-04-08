using Demo.DAL.Models;

namespace Demo.PL.ViewModels
{
    public class HomeViewModel
    {
        public List<Movie> Trending { get; set; } = new List<Movie>();
        public List<Movie> RecentlyAdded { get; set; } = new List<Movie>();
        public List<Movie> TopRated { get; set; } = new List<Movie>();
    }
}
