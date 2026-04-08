namespace Demo.PL.ViewModels
{
    public class UserViewModel
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string? ProfileImageUrl { get; set; }
        public DateTime JoiningDate { get; set; }
        public IEnumerable<ReviewViewModel> LastReviews { get; set; } = new List<ReviewViewModel>();
        public IEnumerable<MovieViewModel> WatchList { get; set; } = new List<MovieViewModel>();
    }
}
