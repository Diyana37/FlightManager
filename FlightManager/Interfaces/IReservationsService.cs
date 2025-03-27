using FlightManager.InputModels.Reservations;
using FlightManager.ViewModels.Reservations;

namespace FlightManager.Interfaces
{
    public interface IReservationsService
    {
        Task CreateAsync(CreateReservationInputModel createReservationInputModel);

        Task<IEnumerable<ReservationBasicViewModel>> GetAllAsync();
    }
}
