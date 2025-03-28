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
        private readonly IFlightsService flightService;

        public ReservationsController(IReservationsService reservationsService, IFlightsService flightService)
        {
            this.reservationsService = reservationsService;
            this.flightService = flightService;
        }
        public async Task<IActionResult> All(string emailFilter, int page = 1, int pageSize = 10)
        {
            var reservations = await this.reservationsService.GetAllAsync(emailFilter, page, pageSize);

            this.ViewBag.EmailFilter = emailFilter; // Store filter value to retain input in the view
            return View(reservations);
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

        public async Task<IActionResult> Create(int count)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToAction("Index", "Home");
            }

            CreateReservationInputModel createReservationInputModel = new CreateReservationInputModel();
            createReservationInputModel.Count = count;

            createReservationInputModel.FlightItems = await this.flightService
                    .GetAllAsItemsAsync();

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
                createReservationInputModel.FlightItems = await this.flightService
                    .GetAllAsItemsAsync();

                return this.View(createReservationInputModel);
            }

            try
            {
                await this.reservationsService.CreateAsync(createReservationInputModel);

                return this.RedirectToAction("All", "Reservations");
            }
            catch (Exception ex)
            {
                createReservationInputModel.FlightItems = await this.flightService
                                    .GetAllAsItemsAsync();

                this.TempData["ErrorMessage"] = ex.Message; 

                return this.View(createReservationInputModel);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            ReservationDetailsViewModel detailsViewModel = await this.reservationsService
                .GetDetailsAsync(id);

            return this.View(detailsViewModel);
        }
    }
}
