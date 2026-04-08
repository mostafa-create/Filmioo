using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FName { get; set; } = default!;
        public string LName { get; set; } = default!;
        public bool IsAgree { get; set; }
        public string? ProfileImageUrl { get; set; }
        public DateTime JoiningDate { get; set; } = DateTime.Now;

        [InverseProperty("ApplicationUser")]
        public virtual ICollection<Review> Reviews { get; } = new HashSet<Review>();
        [InverseProperty("ApplicationUser")]
        public virtual ICollection<WatchListItem> WatchList { get; } = new HashSet<WatchListItem>();
    }
}
