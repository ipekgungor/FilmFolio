using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmFolio.Models
{
    public class Favorite
    {
        [Key]
        public int Id { get; set; }

        public string IdentityUserId { get; set; }

        [ForeignKey("IdentityUserId")]
        public IdentityUser User { get; set; }

        [ValidateNever]
        public int MovieID { get; set; }

        [ForeignKey("MovieID")]
        [ValidateNever]
        public Movie Movie { get; set; }
    }
}
