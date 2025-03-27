using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FlightManager.Data.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(10)]
        [RegularExpression(@"^\d+$", ErrorMessage = "EGN should only contain numbers!")]
        public string EGN { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(255)]
        public string Address { get; set; }
    }
}
