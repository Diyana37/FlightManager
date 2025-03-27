using FlightManager.InputModels.Passengers;
using FlightManager.InputModels.Reservations;
using FlightManager.Interfaces;
using FlightManager.Services;
using FlightManager.ViewModels.Reservations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightManager.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IReservationsService reservationsService;

        public ReservationsController(IReservationsService reservationsService)
        {
            this.reservationsService = reservationsService;
        }
        public async Task<IActionResult> All()
        {
            IEnumerable<ReservationBasicViewModel> reservationBasicViewModels = await this.reservationsService
                .GetAllAsync();

            return this.View(reservationBasicViewModels);
        }

        public IActionResult PassengersCount()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View();
        }

        [HttpPost]
        public IActionResult PassengersCount(CreateReservationInputModel createReservationInputModel)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.RedirectToAction("Create", "Reservations", new { count = createReservationInputModel.Count });
        }

        public IActionResult Create(int count)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home");
            }

            CreateReservationInputModel createReservationInputModel = new CreateReservationInputModel();
            createReservationInputModel.Count = count;

            for (int i = 0; i < count; i++)
            {
                CreatePassengerInputModel createPassengerInputModel = new CreatePassengerInputModel();
                createReservationInputModel.Passengers.Add(createPassengerInputModel);
            }

            return this.View(createReservationInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int count, CreateReservationInputModel createReservationInputModel)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(createReservationInputModel);
            }

            await this.reservationsService.CreateAsync(createReservationInputModel);

            return this.RedirectToAction("All", "Reservations");
        }
    }
}
