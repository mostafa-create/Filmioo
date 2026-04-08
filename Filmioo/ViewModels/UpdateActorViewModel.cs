using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class UpdateActorViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; } = default!;

        public string? LastName { get; set; }

        [DataType(DataType.MultilineText)]
        public string? Bio { get; set; }

        public string? Nationality { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        public string? ExistingImageUrl { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
