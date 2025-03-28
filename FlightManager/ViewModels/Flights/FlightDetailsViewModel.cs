using FlightManager.ViewModels.Passengers;
using FlightManager.ViewModels.Reservations;

namespace FlightManager.ViewModels.Flights
{
    public class FlightDetailsViewModel : FlightBasicViewModel
    {
        public virtual ICollection<ReservationDetailsViewModel> Reservations { get; set; } = new List<ReservationDetailsViewModel>();
    }
}
