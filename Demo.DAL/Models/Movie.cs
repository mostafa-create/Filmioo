using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The movie must have a title.")]
        [StringLength(200)]
        public string Title { get; set; } = default!;
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime? Release_Date { get; set; }
        public DateTime DateAddedToDB { get; set; } = DateTime.Now;

        [StringLength(5000, ErrorMessage = "The Discription is too long.")]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }
        [Display(Name = "YouTube Trailer URL")]
        [Url]
        public string? TrailerUrl { get; set; }
        [Display(Name = "Runtime (Minutes)")]
        public int RuntimeInMinutes { get; set; }
        public decimal Rating { get; set; }
        [Display(Name = "Country of Origin")]
        public string? CountryOfOrigin { get; set; }
        public string? Language { get; set; }       
        public string Author { get; set; } = default!;
        public string Director { get; set; } = default!;
        public string Image_Name { get; set; } = default!;

        [InverseProperty("Movie")]
        public virtual ICollection<Review>? Reviews { get; set; } = new HashSet<Review>();
        [InverseProperty("Movie")]
        public virtual ICollection<MovieGenre>? MovieGenres { get; set; } = new HashSet<MovieGenre>();
        [InverseProperty("Movie")]
        public virtual ICollection<WatchListItem>? WatchList { get; set; } = new HashSet<WatchListItem>();
        [InverseProperty("Movie")]
        public virtual ICollection<MovieActor>? Actors { get; set; } = new HashSet<MovieActor>();
        public bool IsInCurrentUserWatchlist { get; set; } = false;

    }
}
