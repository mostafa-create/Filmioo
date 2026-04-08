using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
    public class MovieGenre
    {
        [ForeignKey(nameof(Models.Genre))]
        public int GenreId { get; set; }
        [ForeignKey(nameof(Models.Movie))]
        public int MovieId { get; set; }
        [InverseProperty("MovieGenres")]
        public Movie Movie { get; set; } = default!;
        [InverseProperty("Movies")]
        public Genre Genre { get; set; } = default!;

    }
}
