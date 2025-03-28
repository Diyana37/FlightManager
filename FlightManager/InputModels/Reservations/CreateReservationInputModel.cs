using FlightManager.InputModels.Passengers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FlightManager.InputModels.Reservations
{
    public class CreateReservationInputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public int Count { get; set; }

        [Required]
        public int FlightId { get; set; }

        public IEnumerable<SelectListItem> FlightItems { get; set; }

        public IList<CreatePassengerInputModel> Passengers { get; set; } = new List<CreatePassengerInputModel>();
    }
}
