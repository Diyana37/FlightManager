using FlightManager.Data.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace FlightManager.InputModels.Passengers
{
    public class CreatePassengerInputModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        public string MiddleName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(10)]
        [RegularExpression(@"^\d+$", ErrorMessage = "EGN should only contain numbers!")]
        public string EGN { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string Nationality { get; set; }

        [Required]
        public string TicketType { get; set; }
    }
}
