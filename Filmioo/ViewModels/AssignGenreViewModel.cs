using Microsoft.AspNetCore.Mvc.Rendering;

namespace Demo.PL.ViewModels
{
    public class AssignGenreViewModel
    {
        public int MovieId { get; set; }
        public IEnumerable<SelectListItem>? MovieList { get; set; }

        public List<int> SelectedGenreIds { get; set; } = new List<int>();
        public IEnumerable<SelectListItem>? GenreList { get; set; }
    }
}
