using FlightManager.Data;
using FlightManager.Data.Entities;
using FlightManager.InputModels.Flights;
using FlightManager.Interfaces;
using FlightManager.Utilities;
using FlightManager.ViewModels.Flights;
using FlightManager.ViewModels.Passengers;
using FlightManager.ViewModels.Reservations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FlightManager.Services
{
    public class FlightsService : IFlightsService
    {
        private readonly ApplicationDbContext dbContext;

        public FlightsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(CreateFlightInputModel createFlightInputModel)
        {
            Flight flight = new Flight
            {
                DepartureAt = createFlightInputModel.DepartureAt,
                LandAt = createFlightInputModel.LandAt,
                CapacityBusiness = createFlightInputModel.CapacityBusiness,
                CapacityStandard = createFlightInputModel.CapacityStandard,
                From = createFlightInputModel.From,
                PilotName = createFlightInputModel.PilotName,
                To = createFlightInputModel.To,
                Type = createFlightInputModel.Type,
                UniqieId = createFlightInputModel.UniqieId
            };

            await this.dbContext.Flights.AddAsync(flight);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Flight flight = await this.dbContext.Flights
                .FirstOrDefaultAsync(f => f.Id == id);

            this.dbContext.Flights.Remove(flight);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task EditAsync(EditFlightInputModel editFlightInputModel)
        {
            Flight flight = await this.dbContext.Flights
                .FirstOrDefaultAsync(f => f.Id == editFlightInputModel.Id);

            flight.DepartureAt = editFlightInputModel.DepartureAt;
            flight.LandAt = editFlightInputModel.LandAt;
            flight.From = editFlightInputModel.From;
            flight.CapacityStandard = editFlightInputModel.CapacityStandard;
            flight.CapacityBusiness = editFlightInputModel.CapacityBusiness;
            flight.PilotName = editFlightInputModel.PilotName;
            flight.To = editFlightInputModel.To;
            flight.UniqieId = editFlightInputModel.UniqieId;
            flight.Type = editFlightInputModel.Type;

            this.dbContext.Flights.Update(flight);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<SelectListItem>> GetAllAsItemsAsync()
        {
            IEnumerable<SelectListItem> selectListItems = await this.dbContext.Flights
                                        .OrderBy(f => f.UniqieId.ToLower())
                                        .Select(i => new SelectListItem
                                        {

                                            Value = i.Id.ToString(),
                                            Text = $"{i.From} - {i.To}({i.UniqieId})"
                                        })
                                        .ToListAsync();

            return selectListItems;
        }

        public async Task<IEnumerable<FlightBasicViewModel>> GetAllAsync()
        {
            IEnumerable<FlightBasicViewModel> flights = await this.dbContext.Flights
                .Select(f => new FlightBasicViewModel
                {
                    DepartureAt = f.DepartureAt,
                    LandAt = f.LandAt,
                    From = f.From,
                    CapacityStandard = f.CapacityStandard,
                    CapacityBusiness = f.CapacityBusiness,
                    PilotName = f.PilotName,
                    To = f.To,
                    UniqieId = f.UniqieId,
                    Type = f.Type,
                    Id = f.Id
                })
                .ToListAsync();

            return flights;
        }

        public async Task<PaginatedList<FlightBasicViewModel>> GetAllAsync(int page, int pageSize)
        {
            var query = this.dbContext.Flights.AsQueryable();

            var filteredQuery = query.Select(f => new FlightBasicViewModel
            {
                DepartureAt = f.DepartureAt,
                LandAt = f.LandAt,
                From = f.From,
                CapacityStandard = f.CapacityStandard,
                CapacityBusiness = f.CapacityBusiness,
                PilotName = f.PilotName,
                To = f.To,
                UniqieId = f.UniqieId,
                Type = f.Type,
                Id = f.Id
            });

            return await PaginatedList<FlightBasicViewModel>.CreateAsync(filteredQuery, page, pageSize);
        }

        public async Task<EditFlightInputModel> GetByIdAsync(int id)
        {
            EditFlightInputModel editFlightInputModel = await this.dbContext.Flights
                .Where(f => f.Id == id)
                .Select(f => new EditFlightInputModel
                {
                    UniqieId = f.UniqieId,
                    DepartureAt = f.DepartureAt,
                    LandAt = f.LandAt,
                    From = f.From,
                    CapacityBusiness = f.CapacityBusiness,
                    PilotName = f.PilotName,
                    To = f.To,
                    CapacityStandard = f.CapacityStandard,
                    Id = f.Id,
                    Type = f.Type
                }).FirstOrDefaultAsync();

            return editFlightInputModel;
        }

        public async Task<FlightDetailsViewModel> GetDetailsAsync(int id)
        {
            FlightDetailsViewModel flightDetailsViewModel = await this.dbContext.Flights
                .Include(f => f.Reservations)
                .Where(f => f.Id == id)
                .Select(f => new FlightDetailsViewModel
                {
                    Id = f.Id,
                    DepartureAt = f.DepartureAt,
                    LandAt = f.LandAt,
                    From = f.From,
                    CapacityBusiness = f.CapacityBusiness,
                    PilotName = f.PilotName,
                    To = f.To,
                    CapacityStandard = f.CapacityStandard,
                    Type = f.Type,
                    UniqieId = f.UniqieId,
                    Reservations = f.Reservations
                        .Select(r => new ReservationDetailsViewModel
                        {
                            Email = r.Email,
                            Flight = r.Flight,
                            FlightId = r.FlightId,
                            Id = r.Id,
                            Passengers = r.Passengers
                            .Select(p => new PassengerViewModel
                            {
                                Id = p.Id,
                                EGN = p.EGN,
                                FirstName = p.FirstName,
                                LastName = p.LastName,
                                MiddleName = p.MiddleName,
                                Nationality = p.Nationality,
                                Phone = p.Phone,
                                TicketType = p.TicketType
                            }).ToList()

                        }).ToList()
                })
                .FirstOrDefaultAsync();

            return flightDetailsViewModel;
        }

    }
}
