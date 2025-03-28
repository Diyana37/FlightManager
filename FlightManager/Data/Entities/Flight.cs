using FlightManager.Attributes;
using System.ComponentModel.DataAnnotations;

namespace FlightManager.Data.Entities
{
    public class Flight
    {
        public Flight()
        {
            this.Reservations = new HashSet<Reservation>();
        }
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string From { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string To { get; set; }

        [Required]
        public DateTime DepartureAt { get; set; }

        [Required]
        [DateGreaterThan("DepartureAt", ErrorMessage = "LandAt must be later than DepartureAt.")]
        public DateTime LandAt { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string UniqieId { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string PilotName { get; set; }

        [Required]
        [Range(1, 400)]
        public int CapacityStandard { get; set; }

        [Required]
        [Range(1, 400)]
        public int CapacityBusiness { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}
