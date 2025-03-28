using FlightManager.InputModels.Reservations;
using FlightManager.Utilities;
using FlightManager.ViewModels.Reservations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FlightManager.Interfaces
{
    public interface IReservationsService
    {
        Task CreateAsync(CreateReservationInputModel createReservationInputModel);

        Task<PaginatedList<ReservationBasicViewModel>> GetAllAsync(string emailFilter, int page, int pageSize);

        Task<ReservationDetailsViewModel> GetDetailsAsync(int id);
    }
}
