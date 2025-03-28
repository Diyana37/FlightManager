using FlightManager.Data.Entities;
using FlightManager.Data.Entities.Enums;
using FlightManager.ViewModels.Flights;
using FlightManager.ViewModels.Passengers;

namespace FlightManager.ViewModels.Reservations
{
    public class ReservationBasicViewModel
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public int FlightId { get; set; }

        public Flight Flight { get; set; }
    }
}
