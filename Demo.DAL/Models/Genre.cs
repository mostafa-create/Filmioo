using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
    public class Genre
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
