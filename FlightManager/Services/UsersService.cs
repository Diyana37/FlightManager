using FlightManager.Data;
using FlightManager.Interfaces;
using FlightManager.Utilities;
using FlightManager.ViewModels.Flights;
using FlightManager.ViewModels.Reservations;
using FlightManager.ViewModels.Users;

namespace FlightManager.Services
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext dbContext;

        public UsersService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<PaginatedList<UserViewModel>> GetAllAsync
            (string emailFilter, string userNameFilter, string firstNameFilter,
                string lastNameFilter, int page, int pageSize)
        {
            var query = this.dbContext.Users.AsQueryable();

            if (!string.IsNullOrEmpty(emailFilter))
            {
                query = query.Where(u => u.Email.Contains(emailFilter));
            }

            if (!string.IsNullOrEmpty(userNameFilter))
            {
                query = query.Where(u => u.UserName.Contains(userNameFilter));
            }

            if (!string.IsNullOrEmpty(firstNameFilter))
            {
                query = query.Where(u => u.FirstName.Contains(firstNameFilter));
            }

            if (!string.IsNullOrEmpty(lastNameFilter))
            {
                query = query.Where(u => u.LastName.Contains(lastNameFilter));
            }

            var filteredQuery = query.Select(u => new UserViewModel
            {
                Id = u.Id,
                Email = u.Email,
                Address = u.Address,
                EGN = u.EGN,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Phone = u.PhoneNumber,
                UserName = u.UserName
            });

            return await PaginatedList<UserViewModel>.CreateAsync(filteredQuery, page, pageSize);
        }
    }
}
