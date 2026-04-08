using Demo.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class ReviewViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "The score is required.")]
        [Range(1, 10, ErrorMessage = "The rating must be between 1 and 10.")]
        [Display(Name = "Your Rating")]
        public decimal? Score { get; set; }
        [StringLength(5000, ErrorMessage = "Your review is too long.")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Your Review")]
        public string? ReviewText { get; set; }
        public DateTime DatePosted { get; set; } = DateTime.Now;
        public string ApplicationUserId { get; set; } = default!;
        public int MovieId { get; set; }

        [InverseProperty("Reviews")]
        public virtual Movie Movie { get; set; } = default!;
        [InverseProperty("Reviews")]
        public virtual ApplicationUser ApplicationUser { get; set; } = default!;
    }
}
