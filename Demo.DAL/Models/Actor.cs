using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
    public class Actor
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

        public string ProfileImageUrl { get; set; } = default!;

        [Display(Name = "Date of birth")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        [InverseProperty("Actor")]
        public virtual ICollection<MovieActor> Movies { get; set; } = new HashSet<MovieActor>();
    }
}
