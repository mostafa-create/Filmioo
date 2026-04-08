using Demo.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class GenreViewModel
    {
        public int GenreId { get; set; }
        [Required(ErrorMessage = "The genre name is required.")]
        [StringLength(50)]
        [Display(Name = "Genre Name")]
        public string Name { get; set; } = default!;
        [InverseProperty("Genre")]
        public virtual ICollection<MovieGenre> Movies { get; } = new HashSet<MovieGenre>();
    }
}
