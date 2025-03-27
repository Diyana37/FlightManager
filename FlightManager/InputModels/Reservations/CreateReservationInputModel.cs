using FlightManager.InputModels.Passengers;
using System.ComponentModel.DataAnnotations;

namespace FlightManager.InputModels.Reservations
{
    public class CreateReservationInputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public int Count { get; set; } 

        public IList<CreatePassengerInputModel> Passengers { get; set; } = new List<CreatePassengerInputModel>();
    }
}
