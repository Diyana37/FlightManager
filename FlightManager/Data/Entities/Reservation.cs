using FlightManager.Data.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace FlightManager.Data.Entities
{
    public class Reservation
    {
        public Reservation()
        {
            this.Passengers = new HashSet<Passenger>();
        }
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public ICollection<Passenger> Passengers { get; set; }

        [Required]
        public int FlightId { get; set; }

        public Flight Flight { get; set; }
    }
}
