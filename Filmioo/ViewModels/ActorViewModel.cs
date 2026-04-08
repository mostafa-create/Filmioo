using Demo.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Demo.PL.ViewModels
{
    public class ActorViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The first name is required.")]
        [StringLength(100)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = default!;

        [StringLength(100)]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Display(Name = "Biography")]
        [DataType(DataType.MultilineText)]
        public string? Bio { get; set; }

        [Display(Name = "Nationality")]
        [StringLength(50, ErrorMessage = "Nationality cannot be longer than 50 characters.")]
        public string? Nationality { get; set; }

        [Required(ErrorMessage = "Please upload a profile picture.")]
        [Display(Name = "Profile Picture")]
        [ValidateNever]
        public string? ProfileImageUrl { get; set; }
        public IFormFile ProfileImageFile { get; set; } = default!;

        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        [InverseProperty("Actor")]
        public virtual ICollection<MovieActor> Movies { get; } = new HashSet<MovieActor>();
    }
}
