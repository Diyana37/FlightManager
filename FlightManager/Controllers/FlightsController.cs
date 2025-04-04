﻿using FlightManager.InputModels.Flights;
using FlightManager.Interfaces;
using FlightManager.ViewModels.Flights;
using FlightManager.ViewModels.Reservations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightManager.Controllers
{
    public class FlightsController : Controller
    {
        private readonly IFlightsService flightsService;
        public FlightsController(IFlightsService flightsService)
        {
            this.flightsService = flightsService;
        }

        public async Task<IActionResult> All(int page = 1, int pageSize = 5)
        {
            var flights = await this.flightsService.GetAllAsync(page, pageSize);

            return View(flights);
        }

        [Authorize(Roles = Constants.ADMINISTRATOR_ROLE)]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize(Roles = Constants.ADMINISTRATOR_ROLE)]
        [HttpPost]
        public async Task<IActionResult> Create(CreateFlightInputModel createFlightInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(createFlightInputModel);
            }

            await this.flightsService.CreateAsync(createFlightInputModel);

            return this.RedirectToAction("All", "Flights");
        }

        [Authorize(Roles = Constants.ADMINISTRATOR_ROLE)]
        public async Task<IActionResult> Edit(int id)
        {
            EditFlightInputModel editFlightInputModel = await this.flightsService
                .GetByIdAsync(id);

            return this.View(editFlightInputModel);
        }

        [Authorize(Roles = Constants.ADMINISTRATOR_ROLE)]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditFlightInputModel editFlightInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(editFlightInputModel);
            }

            await this.flightsService.EditAsync(editFlightInputModel);

            return this.RedirectToAction("All", "Flights");
        }

        [Authorize(Roles = Constants.ADMINISTRATOR_ROLE)]
        public async Task<IActionResult> Delete(int id)
        {
            await this.flightsService.DeleteAsync(id);

            return this.RedirectToAction("All", "Flights");
        }

        public async Task<IActionResult> Details(int id)
        {
            FlightDetailsViewModel detailsViewModel = await this.flightsService
                .GetDetailsAsync(id);

            return this.View(detailsViewModel);
        }
    }
}

