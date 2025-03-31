using FlightManager.Utilities;
using FlightManager.ViewModels.Flights;
using FlightManager.ViewModels.Users;

namespace FlightManager.Interfaces
{
    public interface IUsersService
    {
        Task<PaginatedList<UserViewModel>> GetAllAsync
            (string emailFilter, string userNameFilter, string firstNameFilter,
                string lastNameFilter, int page, int pageSize);
    }
}
