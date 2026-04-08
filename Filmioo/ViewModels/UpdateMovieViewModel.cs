using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class UpdateMovieViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The movie must have a title.")]
        [StringLength(200)]
        public string Title { get; set; } = default!;

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime? Release_Date { get; set; }

        [StringLength(5000)]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }

        [Display(Name = "YouTube Trailer URL"), Url]
        public string? TrailerUrl { get; set; }

        [Display(Name = "Runtime (Minutes)")]
        public int RuntimeInMinutes { get; set; }

        public string? CountryOfOrigin { get; set; }
        public string? Language { get; set; }
        public string Author { get; set; } = default!;
        public string Director { get; set; } = default!;
        public string? ExistingImageName { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
