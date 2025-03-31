using FlightManager.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightManager.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [Authorize(Roles = Constants.ADMINISTRATOR_ROLE)]
        public async Task<IActionResult> All
            (string emailFilter, string userNameFilter, string firstNameFilter,
                string lastNameFilter, int page = 1, int pageSize = 2)
        {
            var users = await this.usersService.
                GetAllAsync(emailFilter, userNameFilter, firstNameFilter,
                    lastNameFilter, page, pageSize);

            this.ViewBag.EmailFilter = emailFilter; // Store filter value to retain input in the view
            this.ViewBag.UserNameFilter = userNameFilter; // Store filter value to retain input in the view
            this.ViewBag.FirstNameFilter = firstNameFilter; // Store filter value to retain input in the view
            this.ViewBag.LastNameFilter = lastNameFilter; // Store filter value to retain input in the view

            return View(users);
        }
    }
}
