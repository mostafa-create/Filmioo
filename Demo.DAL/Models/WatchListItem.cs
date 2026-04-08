using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
    public class WatchListItem
    {
        [ForeignKey(nameof(Models.Movie))]
        public int MovieId { get; set; }
        [ForeignKey(nameof(Models.ApplicationUser))]
        public string UserId { get; set; } = default!;

        [Display(Name = "Title")]
        public string MovieTitle { get; set; } = default!;

        [Display(Name = "Poster")]
        public string? PosterImageUrl { get; set; }
        [InverseProperty("WatchList")]
        public virtual ApplicationUser ApplicationUser { get; set; } = default!;
        [InverseProperty("WatchList")]
        public virtual Movie Movie { get; set; } = default!;

    }
}
