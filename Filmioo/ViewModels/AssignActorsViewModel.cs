using Microsoft.AspNetCore.Mvc.Rendering;

namespace Demo.PL.ViewModels
{
    public class AssignActorsViewModel
    {
        public int MovieId { get; set; }
        public IEnumerable<SelectListItem>? MovieList { get; set; }

        public List<int> SelectedActorIds { get; set; } = new List<int>();
        public IEnumerable<SelectListItem>? ActorList { get; set; }
    }
}
