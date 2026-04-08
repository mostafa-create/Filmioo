using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class EditProfileViewModel
    {
        public string Id { get; set; } = default!;

        [Required, Display(Name = "First Name")]
        public string FName { get; set; } = default!;

        [Required, Display(Name = "Last Name")]
        public string LName { get; set; } = default!;

        public string? Email { get; set; }

        [Display(Name = "Profile Picture")]
        public IFormFile? ImageFile { get; set; }

        public string? ExistingImageUrl { get; set; }
    }
}
