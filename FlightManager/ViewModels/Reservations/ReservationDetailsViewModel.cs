using FlightManager.ViewModels.Passengers;

namespace FlightManager.ViewModels.Reservations
{
    public class ReservationDetailsViewModel : ReservationBasicViewModel
    {
        public virtual ICollection<PassengerViewModel> Passengers { get; set; } = new List<PassengerViewModel>();

    }
}
