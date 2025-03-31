using FlightManager.InputModels.Flights;
using FlightManager.Utilities;
using FlightManager.ViewModels.Flights;
using FlightManager.ViewModels.Reservations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FlightManager.Interfaces
{
    public interface IFlightsService
    {
        Task CreateAsync(CreateFlightInputModel createFlightInputModel);

        Task<PaginatedList<FlightBasicViewModel>> GetAllAsync(int page, int pageSize);

        Task DeleteAsync(int id);

        Task EditAsync(EditFlightInputModel editFlightInputModel);

        Task<EditFlightInputModel> GetByIdAsync(int id);

        Task<IEnumerable<SelectListItem>> GetAllAsItemsAsync();

        Task<FlightDetailsViewModel> GetDetailsAsync(int id);
    }
}
