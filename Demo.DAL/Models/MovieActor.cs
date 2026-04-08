using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
    public class MovieActor
    {
        [ForeignKey(nameof(Models.Actor))]
        public int ActorId { get; set; }
        [ForeignKey(nameof(Models.Movie))]
        public int MovieId { get; set; }
        [InverseProperty("Actors")]
        public Movie Movie { get; set; } = default!;
        [InverseProperty("Movies")]
        public Actor Actor { get; set; } = default!;
    }
}
