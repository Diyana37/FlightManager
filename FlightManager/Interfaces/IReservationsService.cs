using FlightManager.InputModels.Reservations;
using FlightManager.ViewModels.Reservations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FlightManager.Interfaces
{
    public interface IReservationsService
    {
        Task CreateAsync(CreateReservationInputModel createReservationInputModel);

        Task<IEnumerable<ReservationBasicViewModel>> GetAllAsync();

        Task<ReservationDetailsViewModel> GetDetailsAsync(int id);
    }
}
